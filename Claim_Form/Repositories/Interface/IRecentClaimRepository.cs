using Claim_Form.Entities;
using System.Collections;

namespace Claim_Form.Repositories.Interface
{
    /// <summary>
    /// Defines data access operations for recent claims.
    /// </summary>
    public interface IRecentClaimRepository
    {
        /// <summary>
        /// Creates a new claim.
        /// </summary>
        /// <param name="claim">Claim entity.</param>
        /// <returns>Created claim.</returns>
        Task<RecentClaim> CreateAsync(RecentClaim claim);

        /// <summary>
        /// Updates an existing claim.
        /// </summary>
        /// <param name="claim">Claim entity.</param>
        Task UpdateAsync(RecentClaim claim);

        /// <summary>
        /// Retrieves a claim by its identifier.
        /// </summary>
        /// <param name="claimId">Claim identifier.</param>
        /// <returns>Claim if found; otherwise null.</returns>
        Task<RecentClaim?> GetByIdAsync(Guid claimId);
        Task SaveChangesAsync();
        /// <summary>
        /// Retrieves all claims for a specific employee by employee identifier.
        /// </summary>
        /// <param name="empId">Employee identifier.</param>
        /// <returns>List of claims.</returns>
        Task<List<RecentClaim>> GetByEmployeeIdAsync(Guid empId);

        /// <summary>
        /// Retrieves all claims for a specific employee by employee code.
        /// </summary>
        /// <param name="empCode">Employee code.</param>
        /// <returns>List of claims.</returns>
        Task<List<RecentClaim>> GetByEmployeeCodeAsync(string empCode);

        /// <summary>
        /// Deletes draft claims for a specific employee.
        /// </summary>
        /// <param name="empCode">Employee code.</param>
        Task DeleteDraftAsync(string empCode);


        Task ClaimStatusAsync(RecentClaim claim);

        //admin access
        Task<List<RecentClaim?>> GetAllPendingClaims();
    }
}