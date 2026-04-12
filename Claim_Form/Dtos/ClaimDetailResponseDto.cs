using Claim_Form.Entities;

namespace Claim_Form.Dtos
{
    public class ClaimDetailResponseDto
    {

        public Guid RecentClaimId { get; set; }
        public string ClaimType { get; set; } = string.Empty;
        public string? TravelType { get; set; }
        public string ClaimStatus { get; set; } = string.Empty;
        public TravelDetailsDto? TravelDetails { get; set; }

        public List<ExpenseDto> Expenses { get; set; } = new();
        public List<InternationalDto> InternationalExpenses { get; set; } = new();

        public List<DomesticDto>DomesticExpenses { get; set; }= new();
    }


       

    }

