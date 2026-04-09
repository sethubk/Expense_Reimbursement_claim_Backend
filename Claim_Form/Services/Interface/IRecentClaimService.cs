using Claim_Form.Dtos;

namespace Claim_Form.Services.Interface
{
    /// <summary>
    /// Defines business operations related to recent claims.
    /// </summary>
    public interface IRecentClaimService
    {
        /// <summary>
        /// Creates a new claim for an employee.
        /// </summary>
        /// <param name="dto">Claim creation details.</param>
        /// <param name="empCode">Employee code.</param>
        /// <returns>Created claim details.</returns>
        Task<RecentClaimResponseDto> CreateClaimAsync(
            RecentClaimDto dto,
            string empCode);

        /// <summary>
        /// Updates an existing claim.
        /// </summary>
        /// <param name="dto">Fields to update.</param>
        /// <param name="empCode">Employee code.</param>
        /// <param name="claimId">Claim identifier.</param>
        /// <returns>Updated claim details if found; otherwise null.</returns>
        Task<RecentClaimDto?> UpdateClaimAsync(
            UpdateClaimDto dto,
            string empCode,
            Guid claimId);

        /// <summary>
        /// Retrieves a claim by its identifier.
        /// </summary>
        /// <param name="id">Claim identifier.</param>
        /// <returns>Claim details if found; otherwise null.</returns>
        Task<ClaimDetailResponseDto?> GetClaimAsync(Guid id);

        /// <summary>
        /// Retrieves all claims for an employee by employee identifier.
        /// </summary>
        /// <param name="empId">Employee identifier.</param>
        /// <returns>List of claims.</returns>
        Task<List<RecentClaimResponseDto>> GetClaimByEmpIDAsync(Guid empId);

        /// <summary>
        /// Retrieves all claims for an employee by employee code.
        /// </summary>
        /// <param name="empCode">Employee code.</param>
        /// <returns>List of claims.</returns>
        Task<List<RecentClaimResponseDto>> GetClaimByEmpCodeAsync(string empCode);

        /// <summary>
        /// Deletes draft claims for a specific employee.
        /// </summary>
        /// <param name="empCode">Employee code.</param>
        /// <returns>True if deleted; false if nothing to delete.</returns>
        Task<bool> DeleteDraftAsync(string empCode);
    }
}