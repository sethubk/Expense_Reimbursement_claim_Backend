namespace Claim_Form.Dtos
{
    /// <summary>
    /// Represents claim details returned to the client.
    /// </summary>
    public class RecentClaimResponseDto
    {
        /// <summary>
        /// Unique identifier of the claim.
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
    }
}