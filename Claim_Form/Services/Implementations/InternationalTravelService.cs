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
            if (claim1.TravelDetails == null)
            {
                //var travel = _mapper.Map<TravelDetails>(travelDetailsDtos);
                //travel.RecentClaimId = ClaimID;
                //var created = await _internationalTravelRepository.AddTravelDetails(travel);
                //return _mapper.Map<TravelDetailsDtos>(created);
                var entity = new TravelDetails
                {
                   
                    CurrecncyType = travelDetailsDtos.CurrencyType,
                    TravelStartDate = travelDetailsDtos.TravelStartDate,
                    TravelEndDate = travelDetailsDtos.TravelEndDate,
                    TotalDays = travelDetailsDtos.TotalDays,
                    RecentClaimId = claim1.RecentClaimId,

                    CardCashEntries = travelDetailsDtos.CardCashEntries.Select(c => new CashInfo
                        {
                            LoadedDate=c.TotalLoaded,
                            Type = c.Type,
                            INRRate = c.INRRate,
                            TotalLoaded = c.TotalLoaded,
                        }).ToList()
       
            }; var created = await _internationalTravelRepository.AddTravelDetails(entity);
                return _mapper.Map<TravelDetailsDtos>(entity);
            }
           
            else
            {
                return null;

            }
            //return  return _mapper.Map<TravelDetailsDtos>(created);


        }
    }
}
