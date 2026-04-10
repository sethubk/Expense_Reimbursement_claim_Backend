namespace Claim_Form.Dtos
{
    /// <summary>
    /// Represents an expense entry associated with a claim.
    /// </summary>
    public class ExpenseDto
    {
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
        /// Mode of payment (Cash, Card, Online, etc.).
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
        /// Screenshot or receipt file path / URL.
        /// </summary>
        public string Screenshot { get; set; } = string.Empty;
    }
}