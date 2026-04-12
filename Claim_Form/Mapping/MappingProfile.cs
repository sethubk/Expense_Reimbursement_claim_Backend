using AutoMapper;
using Claim_Form.Dtos;
using Claim_Form.Entities;

namespace Claim_Form.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<TravelDetails, TravelDetailsDto>();
            CreateMap<TravelDetailsDto, TravelDetails>();
            CreateMap<RecentClaim, RecentClaimResponseDto>();
            CreateMap<Employee,EmployeeResponseDto>();

            CreateMap<RecentClaim, RecentClaimDto>();
            // Parent mapping
            CreateMap<Employee, EmpWithClaimDto>()
                .ForMember(
                    destination => destination.RecentClaims,
                    options => options.MapFrom(source => source.RecentClaims)
                );

            
            CreateMap<Expense, ExpenseDto>();

          



            CreateMap<International, InternationalDto>();

            //child mapping for travel details
            CreateMap<CashInfoDto, CashInfo>();
            //parent mapping

            CreateMap<TravelDetailsDto, TravelDetails>()
                       .ForMember(d => d.TravelId, o => o.Ignore())
                       .ForMember(d => d.ReimbursementStatus, o => o.Ignore());



            // Child mapping (replaces Select)
            CreateMap<CashInfo, CashInfoDto>();

            // Parent mapping
            //CreateMap<TravelDetails, TravelDetailsDto>();

            //CreateMap<RecentClaimDto, RecentClaim>()
            //           .ForMember(d => d.RecentClaimId, o => o.Ignore());

            CreateMap<RecentClaim, RecentClaimResponseDto>();
            CreateMap<RecentClaim, RecentClaimDto>();

            CreateMap<Domestic,DomesticDto>();

        }
    }
}
