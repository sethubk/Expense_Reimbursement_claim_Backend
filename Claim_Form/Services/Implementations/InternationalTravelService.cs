using AutoMapper;
using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics;
using System.Security.Claims;

namespace Claim_Form.Services.Implementations
{
    public class InternationalTravelService:IInternationalTravelService
    {

        private readonly IInternationalTravelRepository _internationalTravelRepository;
        private readonly IMapper _mapper;
        private readonly IRecentClaimRepository _RecentRepository;

        public InternationalTravelService(IInternationalTravelRepository internationalTravelRepository,IMapper mapper,IRecentClaimRepository recentClaimRepository)
        {
            _internationalTravelRepository = internationalTravelRepository;
            _mapper = mapper;
            _RecentRepository = recentClaimRepository;
        }
        public async Task<TravelDetailsDtos>AddTravelDetails( Guid ClaimID, TravelDetailsDtos travelDetailsDtos)
        {
            var claim1 = await _RecentRepository.GetClaim(ClaimID);
            if (claim1 == null)
            {
                throw new ArgumentNullException(nameof(claim1));
            }
            var travel1= await _internationalTravelRepository.GetTravel(ClaimID);
            if (travel1 == null)
            {
                //var travel = _mapper.Map<TravelDetails>(travelDetailsDtos);
                //travel.RecentClaimId = ClaimID;
                //var created = await _internationalTravelRepository.AddTravelDetails(travel);
                //return _mapper.Map<TravelDetailsDtos>(created);
                var travel = new TravelDetails
                {
                    CurrecncyType = travelDetailsDtos.CurrencyType,
                    TravelStartDate = travelDetailsDtos.TravelStartDate,
                    TravelEndDate = travelDetailsDtos.TravelEndDate,
                    TotalDays = travelDetailsDtos.TotalDays,
                    AdvanceAmount = travelDetailsDtos.AdvanceAmount,
                    RecentClaimId = claim1.RecentClaimId,
                    ReimbersementStatus = "Pending",
                    CardCashEntries = travelDetailsDtos.CardCashEntries.Select(c => new CashInfo
                        {
                            LoadedDate=c.LoadedDate,
                            Type = c.Type,
                            INRRate = c.INRRate,
                            TotalLoaded = c.TotalLoaded,
                        }).ToList()
       
            }; 
               await _internationalTravelRepository.AddTravelDetails(travel);
                //return _mapper.Map<TravelDetailsDtos>(travel);

                return new TravelDetailsDtos
                {
                    
                    CurrencyType = travel.CurrecncyType,  // spelling fix
                    TravelStartDate = travel.TravelStartDate,
                    TravelEndDate = travel.TravelEndDate,
                    TotalDays = travel.TotalDays,
                    AdvanceAmount= travel.AdvanceAmount,
                  

                    CardCashEntries = travel.CardCashEntries.Select(c => new CashInfoDtos
                    {
                        LoadedDate = c.TotalLoaded,
                        Type = c.Type,
                        INRRate = c.INRRate,
                        TotalLoaded = c.TotalLoaded,
                    }).ToList()
                };

            }

            else
            {

                var travel = claim1.TravelDetails; // Assuming 1:1 per claim
              
                foreach (var c in travelDetailsDtos.CardCashEntries)
                {
                    travel.CardCashEntries.Add(new CashInfo
                    {
                        LoadedDate = c.LoadedDate,
                        Type = c.Type,
                        INRRate = c.INRRate,
                        TotalLoaded = c.TotalLoaded,
                        TravelId = travel.TravelID

                    });
                }
                travel.AdvanceAmount = travelDetailsDtos.AdvanceAmount;
                await _internationalTravelRepository.UpdateTravelDetails(travel);

                // return updated DTO
                return new TravelDetailsDtos
                {
                    
                    CurrencyType = travel.CurrecncyType,
                    TravelStartDate = travel.TravelStartDate,
                    TravelEndDate = travel.TravelEndDate,
                    TotalDays = travel.TotalDays,
                    CardCashEntries = travel.CardCashEntries.Select(c => new CashInfoDtos
                    {
                        LoadedDate = c.LoadedDate,
                        Type = c.Type,
                        INRRate = c.INRRate,
                        TotalLoaded = c.TotalLoaded
                    }).ToList()
                };

            }
            //return  return _mapper.Map<TravelDetailsDtos>(created);

        }
        public async Task<TravelDetailsDtos?> GetTravelByClaimId(Guid claimId)
        {
            var travel = await _internationalTravelRepository.GetTravelByClaimId(claimId);

            if (travel == null)
                return null;

            // MANUAL MAPPING (clean & complete)
            return new TravelDetailsDtos
            {
                
                CurrencyType = travel.CurrecncyType,
                TravelStartDate = travel.TravelStartDate,
                TravelEndDate = travel.TravelEndDate,
                TotalDays = travel.TotalDays,
                AdvanceAmount=travel.AdvanceAmount,
                CardCashEntries = travel.CardCashEntries.Select(c => new CashInfoDtos
                {
                    LoadedDate = c.LoadedDate,
                    Type = c.Type,
                    INRRate = c.INRRate,
                    TotalLoaded = c.TotalLoaded
                }).ToList()
            };
        }

    }
}
