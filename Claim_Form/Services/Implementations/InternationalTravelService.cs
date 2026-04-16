using AutoMapper;
using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Claim_Form.Services.Interface;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="InternationalTravelService"/> class.
        /// </summary>
        public InternationalTravelService(
            IInternationalTravelRepository travelRepository,
            IRecentClaimRepository claimRepository,
            IMapper mapper)
        {
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
            //if (travel != null)
            //{
            //    if(travel.CardCashEntries==c)
            //}
            await _travelRepository.DeleteCardCashEntriesAsync(travel.TravelId);
            var claim1 = await _claimRepository.GetByIdAsync(claimId);
            if (claim1 == null)
                throw new InvalidOperationException("Claim not found.");

            var travel1 = claim1.TravelDetails;

            travel1.CurrencyType = dto.CurrencyType;
            travel1.TravelStartDate = dto.TravelStartDate;
            travel1.TravelEndDate = dto.TravelEndDate;
            travel1.TotalDays = dto.TotalDays;
            travel1.AdvanceAmount = dto.AdvanceAmount;
            travel1.ReimbursementStatus = "Pending";

          
           

            foreach (var entry in dto.CardCashEntries)
            {
               // var exiting = travel.CardCashEntries.FirstOrDefault(x=>x.Id == entry.)
                travel1.CardCashEntries.Add(new CashInfo
                {
                    LoadedDate = entry.LoadedDate,
                    PaymentType = entry.PaymentType,
                    InrRate = entry.InrRate,
                    TotalLoadedAmount = entry.TotalLoadedAmount
                });
            }

           
            await _travelRepository.UpdateAsync(travel1);

            return _mapper.Map<TravelDetailsDto>(travel1);
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