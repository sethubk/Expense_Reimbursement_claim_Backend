namespace Claim_Form.Dtos
{
    /// <summary>
    /// Represents data required to update an existing claim.
    /// </summary>
    public class UpdateClaimDto
    {
        /// <summary>
        /// Updated status of the claim (e.g., Draft, Submitted, Approved).
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Updated total amount of the claim.
        /// </summary>
        public decimal Amount { get; set; }
    }
}
