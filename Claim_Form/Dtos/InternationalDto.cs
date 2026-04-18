namespace Claim_Form.Dtos
{
    /// <summary>
    /// Represents an international expense entry associated with a claim.
    /// </summary>
    public class InternationalDto
    {
        /// <summary>
        /// Date on which the expense was incurred.
        /// </summary>
        /// 

        public Guid? Id {  get; set; }
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
        /// Mode of payment (Cash, Card, Online, etc.).
        /// Stored as string as per current requirement.
        /// </summary>
        public string PaymentMode { get; set; } = string.Empty;

        /// <summary>
        /// Currency type used for the transaction (e.g., USD, EUR, SGD).
        /// </summary>
        public string CurrencyType { get; set; } = string.Empty;

        /// <summary>
        /// Expense amount in foreign currency.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Converted amount in INR.
        /// </summary>
        public decimal ConvertedAmount { get; set; }

        /// <summary>
        /// Additional remarks or notes.
        /// </summary>
        public string Remarks { get; set; } = string.Empty;

        /// <summary>
        /// Screenshot or receipt file path / Base64 string.
        /// </summary>
        public string Screenshot { get; set; } = string.Empty;
    }
}