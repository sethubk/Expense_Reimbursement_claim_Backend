using AutoMapper;
using Claim_Form.Data;
using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Claim_Form.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Claim_Form.Services.Implementations
{
    /// <summary>
    /// Service implementation for international travel details operations.
    /// </summary>
    public class InternationalTravelService : IInternationalTravelService
    {
        private readonly IInternationalTravelRepository _travelRepository;
        private readonly IRecentClaimRepository _claimRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternationalTravelService"/> class.
        /// </summary>
        public InternationalTravelService(
            IInternationalTravelRepository travelRepository,
            IRecentClaimRepository claimRepository,
            IMapper mapper,
            AppDbContext context
            )
        {
            _context=context;
            _travelRepository = travelRepository;
            _claimRepository = claimRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds or updates travel details for a claim.
        /// </summary>
        public async Task<TravelDetailsDto> AddTravelDetailsAsync(
     Guid claimId,
     TravelDetailsDto dto)
        {
            var claim = await _claimRepository.GetByIdAsync(claimId);
            if (claim == null)
                throw new InvalidOperationException("Claim not found.");

            var travel = claim.TravelDetails;

            _context.ChangeTracker.Clear();
            if (travel == null)
            {
                travel = new TravelDetails
                {
                    RecentClaimId = claimId,
                    CurrencyType = dto.CurrencyType,
                    TravelStartDate = dto.TravelStartDate,
                    TravelEndDate = dto.TravelEndDate,
                    TotalDays = dto.TotalDays,
                    AdvanceAmount = dto.AdvanceAmount,
                    ReimbursementStatus = "Pending",
                    CardCashEntries = dto.CardCashEntries.Select(c => new CashInfo
                    {
                        LoadedDate = c.LoadedDate,
                        PaymentType = c.PaymentType,
                        InrRate = c.InrRate,
                        TotalLoadedAmount = c.TotalLoadedAmount
                    }).ToList()
                };

                await _travelRepository.AddAsync(travel);
                return dto;
            }
            // =========================
            // UPDATE MASTER
            // =========================
            travel.CurrencyType = dto.CurrencyType;
            travel.TravelStartDate = dto.TravelStartDate;
            travel.TravelEndDate = dto.TravelEndDate;
            travel.TotalDays = dto.TotalDays;
            travel.AdvanceAmount = dto.AdvanceAmount;

            var existingEntries = travel.CardCashEntries
                .Where(x => !x.IsDeleted)
                .ToList();

            var incomingIds = dto.CardCashEntries
                .Where(x => x.Id != null && x.Id != Guid.Empty)
                .Select(x => x.Id.Value)
                .ToList();

            // =========================
            // UPDATE + INSERT
            // =========================
            foreach (var item in dto.CardCashEntries)
            {
                if (item.Id != null && item.Id != Guid.Empty)
                {
                    // ✅ ONLY USE TRACKED ENTITY
                    var existing = existingEntries
                        .FirstOrDefault(x => x.Id == item.Id);

                    if (existing != null)
                    {
                        existing.LoadedDate = item.LoadedDate;
                        existing.PaymentType = item.PaymentType;
                        existing.InrRate = item.InrRate;
                        existing.TotalLoadedAmount = item.TotalLoadedAmount;

                        existing.ModifiedAt = DateTime.UtcNow;
                        existing.ModifiedBy = "SYSTEM";
                    }
                }
                else
                {
                    // ✅ INSERT
                    travel.CardCashEntries.Add(new CashInfo
                    {
                        Id = Guid.NewGuid(),
                        TravelId = travel.TravelId, // MUST
                        LoadedDate = item.LoadedDate,
                        PaymentType = item.PaymentType,
                        InrRate = item.InrRate,
                        TotalLoadedAmount = item.TotalLoadedAmount,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "SYSTEM"
                    });
                }
            }

            // =========================
            // SOFT DELETE
            // =========================
            var toDelete = existingEntries
                .Where(x => !incomingIds.Contains(x.Id))
                .ToList();

            foreach (var item in toDelete)
            {
                item.IsDeleted = true;
                item.DeletedAt = DateTime.UtcNow;
                item.DeletedBy = "SYSTEM";
            }

            // =========================
            // SAVE
            // =========================
            await _context.SaveChangesAsync();

            return _mapper.Map<TravelDetailsDto>(travel);
        }

        // ✅ Return updated DTO
        //return new TravelDetailsDto
        //{
        //    CurrencyType = travel.CurrencyType,
        //    TravelStartDate = travel.TravelStartDate,
        //    TravelEndDate = travel.TravelEndDate,
        //    TotalDays = travel.TotalDays,
        //    AdvanceAmount = travel.AdvanceAmount,
        //    CardCashEntries = travel.CardCashEntries.Select(c => new CashInfoDto
        //    {
        //        LoadedDate = c.LoadedDate,
        //        PaymentType = c.PaymentType,
        //        InrRate = c.InrRate,
        //        TotalLoadedAmount = c.TotalLoadedAmount
        //    }).ToList()
        //};


    //    public async Task<TravelDetailsDto> UpdateTravelDetailsAsync(
    //Guid claimId,
    //TravelInputDto dto)
    //    {
    //        var travel = await _travelRepository.GetByClaimIdAsync(claimId);
    //        travel.CurrencyType = dto.CurrencyType;
    //        travel.TravelStartDate = dto.TravelStartDate;
    //        travel.TravelEndDate = dto.TravelEndDate;
    //        travel.TotalDays = dto.TotalDays;
    //        travel.AdvanceAmount = dto.AdvanceAmount;
    //        travel.ReimbursementStatus = "Pending";




    //        foreach (var entry in dto.CardCashEntries)
    //        {
    //            var exiting = travel.CardCashEntries.FirstOrDefault(x => x.Id == entry.id);
    //            if(exiting == null)
    //            {
    //                travel.CardCashEntries.Add(new CashInfo
    //                {
    //                    LoadedDate = entry.LoadedDate,
    //                    PaymentType = entry.PaymentType,
    //                    InrRate = entry.InrRate,
    //                    TotalLoadedAmount = entry.TotalLoadedAmount
    //                });
    //            }

    //            else
    //            {

    //            }
    //        }


    //        await _travelRepository.UpdateAsync(travel);

    //        return _mapper.Map<TravelDetailsDto>(travel);
    //    }

        /// <summary>
        /// Retrieves travel details by claim identifier.
        /// </summary>
        public async Task<TravelDetailsDto?> GetTravelByClaimIdAsync(Guid claimId)
        {
            var travel = await _travelRepository.GetByClaimIdAsync(claimId);

            if (travel == null)
                return null;

            //return new TravelDetailsDto
            //{
            //    CurrencyType = travel.CurrencyType,
            //    TravelStartDate = travel.TravelStartDate,
            //    TravelEndDate = travel.TravelEndDate,
            //    TotalDays = travel.TotalDays,
            //    AdvanceAmount = travel.AdvanceAmount,
            //    CardCashEntries = travel.CardCashEntries.Select(c => new CashInfoDto
            //    {
            //        LoadedDate = c.LoadedDate,
            //        PaymentType = c.PaymentType,
            //        InrRate = c.InrRate,
            //        TotalLoadedAmount = c.TotalLoadedAmount
            //    }).ToList()
            //};
            return _mapper.Map<TravelDetailsDto>(travel);

        }
    }
}