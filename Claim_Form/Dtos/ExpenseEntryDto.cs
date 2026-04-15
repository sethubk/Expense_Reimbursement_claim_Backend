namespace Claim_Form.Dtos
{
    /// <summary>
    /// Represents an individual expense entry used during bulk expense creation.
    /// </summary>
    public class ExpenseEntryDto
    {
        /// <summary>
        /// Expense amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Date on which the expense was incurred.
        /// Accepts ISO format (e.g., 2026-03-03).
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Description or particulars of the expense.
        /// </summary>
        public string Particulars { get; set; } = string.Empty;

        /// <summary>
        /// Mode of payment (Cash, Card, Online, etc.).
        /// </summary>
        public string PaymentMode { get; set; } = string.Empty;

        /// <summary>
        /// Additional remarks or notes.
        /// </summary>
        public string Remarks { get; set; } = string.Empty;

        /// <summary>
        /// Supporting reference number such as invoice or receipt number.
        /// </summary>
        public string SupportingNo { get; set; } = string.Empty;

        /// <summary>
        /// Original file name of the uploaded receipt or document.
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Screenshot or receipt file path / Base64 string.
        /// Nullable when no attachment is provided.
        /// </summary>
        public IFormFile? Screenshot { get; set; }
    }
}