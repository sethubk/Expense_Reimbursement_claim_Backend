using AutoMapper;
using Claim_Form.Dtos;
using Claim_Form.Entities;

namespace Claim_Form.Mapping
{
    public class AutoMapper:Profile
    {
        public AutoMapper() {
            CreateMap<TravelDetails, TravelDetailsDtos>();
            CreateMap<TravelDetailsDtos, TravelDetails>();
        }
    }
}
