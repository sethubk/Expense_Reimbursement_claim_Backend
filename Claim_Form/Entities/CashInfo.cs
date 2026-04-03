namespace Claim_Form.Entities
{
    /// <summary>
    /// Represents cash or card loading information associated with travel details.
    /// </summary>
    public class CashInfo
    {
        /// <summary>
        /// Unique identifier for the cash information record.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Date when the cash or card amount was loaded.
        /// </summary>
        public DateTime LoadedDate { get; set; }

        /// <summary>
        /// Type of payment method (Cash/Card).
        /// </summary>
        public string PaymentType { get; set; } = string.Empty;

        /// <summary>
        /// INR conversion rate applied at the time of loading.
        /// </summary>
        public decimal InrRate { get; set; }

        /// <summary>
        /// Total amount loaded in foreign currency.
        /// </summary>
        public decimal TotalLoadedAmount { get; set; }

        /// <summary>
        /// Foreign key reference to TravelDetails.
        /// </summary>
        public Guid TravelId { get; set; }

        /// <summary>
        /// Navigation property to associated TravelDetails.
        /// </summary>
        public TravelDetails TravelDetails { get; set; } = null!;
    }
}