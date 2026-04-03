namespace Claim_Form.Dtos
{
    /// <summary>
    /// Represents cash or card loading information for travel expenses.
    /// </summary>
    public class CashInfoDto
    {
        /// <summary>
        /// Date when the cash/card amount was loaded.
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
    }
}