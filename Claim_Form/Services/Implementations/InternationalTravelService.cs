using AutoMapper;
using Claim_Form.Data;
using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Repositories.Implementations;
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
           
            travel.CurrencyType = dto.CurrencyType;
            travel.TravelStartDate = dto.TravelStartDate;
            travel.TravelEndDate = dto.TravelEndDate;
            travel.TotalDays = dto.TotalDays;
            travel.AdvanceAmount = dto.AdvanceAmount;

            var existingEntries = travel.CardCashEntries
                .Where(x => !x.IsDeleted)
                .ToList();

            var incomingIds = dto.CardCashEntries
                .Where(x => x.Id.HasValue && x.Id != Guid.Empty)
                .Select(x => x.Id.Value)
                .ToList();

            
            foreach (var item in dto.CardCashEntries)
            {
                // UPDATE
                if (item.Id.HasValue && item.Id != Guid.Empty)
                {
                    var existing = existingEntries
                        .FirstOrDefault(x => x.Id == item.Id);

                    if (existing != null)
                    {
                        existing.LoadedDate = item.LoadedDate;
                        existing.PaymentType = item.PaymentType;
                        existing.InrRate = item.InrRate;
                        existing.TotalLoadedAmount = item.TotalLoadedAmount;
                        await _travelRepository.UpdateCashInfoAsync(existing);
                    }
                    
                }
                // INSERT
                else
                {
                    var newEntity = new CashInfo
                    {
                        Id = Guid.NewGuid(),
                        TravelId=travel.TravelId,
                        LoadedDate = item.LoadedDate,
                        PaymentType = item.PaymentType,
                        InrRate = item.InrRate,
                        TotalLoadedAmount = item.TotalLoadedAmount,
                        IsDeleted = false
                    };

                    await _travelRepository.AddCashInfoAsync(newEntity);
                }
            }

       
            var toDelete = existingEntries
                .Where(x => !incomingIds.Contains(x.Id))
                .ToList();

            foreach (var item in toDelete)
            {
               
                item.IsDeleted = true;
                item.DeletedAt = DateTime.UtcNow;
              
                await _travelRepository.UpdateCashInfoAsync(item);
            }

           
            await _travelRepository.SaveChangesAsync();


            return _mapper.Map<TravelDetailsDto>(travel);
        }

        

        /// <summary>
        /// Retrieves travel details by claim identifier.
        /// </summary>
        public async Task<TravelDetailsDto?> GetTravelByClaimIdAsync(Guid claimId)
        {
            var travel = await _travelRepository.GetByClaimIdAsync(claimId);

            if (travel == null)
                return null;

         
            return _mapper.Map<TravelDetailsDto>(travel);

        }
    }
}