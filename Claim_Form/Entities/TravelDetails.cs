using System.ComponentModel.DataAnnotations;

namespace Claim_Form.Entities
{
    /// <summary>
    /// Represents travel details associated with a claim.
    /// </summary>
    public class TravelDetails
    {
        /// <summary>
        /// Unique identifier for the travel details record.
        /// </summary>
        [Key]
        public Guid TravelId { get; set; }

        /// <summary>
        /// Currency type used during travel (e.g., USD, EUR).
        /// </summary>
        public string CurrencyType { get; set; } = string.Empty;

        /// <summary>
        /// Travel start date.
        /// </summary>
        public DateTime TravelStartDate { get; set; }

        /// <summary>
        /// Travel end date.
        /// </summary>
        public DateTime TravelEndDate { get; set; }

        /// <summary>
        /// Total number of travel days.
        /// </summary>
        public int TotalDays { get; set; }

        /// <summary>
        /// Advance amount taken before travel.
        /// </summary>
        public decimal AdvanceAmount { get; set; }

        /// <summary>
        /// Reimbursement status (Pending, Settled, Rejected, etc.).
        /// </summary>
        public string ReimbursementStatus { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key reference to the related claim.
        /// </summary>
        public Guid RecentClaimId { get; set; }

        /// <summary>
        /// Navigation property to the related claim.
        /// </summary>
        public RecentClaim RecentClaim { get; set; } = null!;

        /// <summary>
        /// Cash or card entries associated with this travel.
        /// </summary>
        public ICollection<CashInfo> CardCashEntries { get; set; } = new List<CashInfo>();

        /// <summary>
        /// International expense entries associated with this travel.
        /// </summary>
        public ICollection<International> Internationals { get; set; } = new List<International>();


        public ICollection<Domestic>Domestics { get; set; }=new List<Domestic>();
    }
}
