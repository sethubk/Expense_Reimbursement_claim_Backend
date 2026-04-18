using System.ComponentModel.DataAnnotations;

namespace Claim_Form.Entities
{
    public class Domestic:AuditInfo
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Date on which the expense was incurred.
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Supporting reference number such as invoice or receipt number.
        /// </summary>
        public string SupportingNo { get; set; } = string.Empty;

        /// <summary>
        /// Description or particulars of the expense.
        /// </summary>
        public string Particulars { get; set; } = string.Empty;

        /// <summary>
        /// Mode of payment used for the expense (Cash, Card, Online, etc.).
        /// </summary>
        public string PaymentMode { get; set; } = string.Empty;

        /// <summary>
        /// Expense amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Additional remarks or notes.
        /// </summary>
        public string Remarks { get; set; } = string.Empty;

        /// <summary>
        /// Screenshot or receipt file path.
        /// </summary>
        public string Screenshot { get; set; } = string.Empty;
        /// <summary>
        /// Foreign key reference to TravelDetails.
        /// </summary>
        public Guid TravelId { get; set; }

        /// <summary>
        /// Navigation property to related TravelDetails.
        /// </summary>
        public TravelDetails TravelDetails { get; set; } = null!;

    }
}
