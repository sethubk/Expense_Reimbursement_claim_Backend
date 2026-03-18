using System.ComponentModel.DataAnnotations;

namespace Claim_Form.Entities
{

    public class TravelDetails
    {
        [Key]
        public Guid TravelID { get; set; }
        public string CurrecncyType { get; set; }
        public string TravelStartDate { get; set; }
        public string TravelEndDate { get; set; }
        public string TotalDays { get; set; }
        public Guid RecentClaimId { get; set; }
        public RecentClaim RecentClaim { get; set; }

        
        public List<CashInfo> CardCashEntries { get; set; } = new();

    }
}
