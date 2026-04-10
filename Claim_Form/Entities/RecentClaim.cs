namespace Claim_Form.Entities
{
    /// <summary>
    /// Represents a claim created by an employee.
    /// </summary>
    public class RecentClaim
    {
        /// <summary>
        /// Unique identifier for the claim.
        /// </summary>
        public Guid RecentClaimId { get; set; }

        /// <summary>
        /// Type of claim (e.g., Domestic, International).
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Date when the claim was created or submitted.
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Purpose or reason for the claim.
        /// </summary>
        public string Purpose { get; set; } = string.Empty;

        /// <summary>
        /// Total claim amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Current status of the claim (Draft, Submitted, Approved, Rejected).
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key reference to the employee who owns the claim.
        /// </summary>
        public Guid EmpId { get; set; }

        /// <summary>
        /// Navigation property to the employee.
        /// </summary>
        public Employee Employee { get; set; } = null!;

        /// <summary>
        /// Expenses associated with the claim.
        /// </summary>
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();

        /// <summary>
        /// Travel details associated with the claim.
        /// </summary>
        public TravelDetails TravelDetails { get; set; } = null!;
    }
}