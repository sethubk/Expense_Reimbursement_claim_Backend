using Claim_Form.Entities;

namespace Claim_Form.Dtos
{
    public class TravelDetailsDtos
    {
        public string CurrencyType { get; set; }
        public string TravelStartDate { get; set; }
        public string TravelEndDate { get; set; }
        public string TotalDays { get; set; } 
        public Guid RecentClaimId { get; set; }

        public List<CashInfoDtos> CardCashEntries { get; set; }
    }
}
