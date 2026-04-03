using AutoMapper;
using Claim_Form.Dtos;
using Claim_Form.Entities;

namespace Claim_Form.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<TravelDetails, TravelDetailsDtos>();
            CreateMap<TravelDetailsDtos, TravelDetails>();
            CreateMap<RecentClaim, RecentClaimResponseDto>();
        }
    }
}
