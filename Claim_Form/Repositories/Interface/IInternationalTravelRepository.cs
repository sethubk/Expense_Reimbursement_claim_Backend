using Claim_Form.Entities;

namespace Claim_Form.Repositories.Interface
{
    /// <summary>
    /// Defines data access operations for travel details related to international claims.
    /// </summary>
    public interface IInternationalTravelRepository
    {
        /// <summary>
        /// Adds new travel details for a claim.
        /// </summary>
        /// <param name="travelDetails">Travel details entity.</param>
        Task AddAsync(TravelDetails travelDetails);

        /// <summary>
        /// Updates existing travel details.
        /// </summary>
        /// <param name="travelDetails">Travel details entity.</param>
        /// <returns>Updated travel details.</returns>
        Task<TravelDetails> UpdateAsync(TravelDetails travelDetails);

        /// <summary>
        /// Updates the reimbursement status for a specific travel record.
        /// </summary>
        /// <param name="travelId">Travel identifier.</param>
        /// <param name="status">New reimbursement status.</param>
        /// <returns>Updated travel details if found; otherwise null.</returns>
        Task<TravelDetails?> UpdateReimbursementStatusAsync(Guid travelId, string status);

        /// <summary>
        /// Retrieves travel details for a given claim identifier.
        /// </summary>
        /// <param name="claimId">Claim identifier.</param>
        /// <returns>Travel details if found; otherwise null.</returns>
        Task<TravelDetails?> GetByClaimIdAsync(Guid claimId);

        /// <summary>
        /// Retrieves travel details by travel identifier.
        /// </summary>
        /// <param name="travelId">Travel identifier.</param>
        /// <returns>Travel details if found; otherwise null.</returns>
        Task<TravelDetails?> GetByIdAsync(Guid travelId);

        Task<CashInfo?> GetCashInfoByIdAsync(Guid id);

        Task AddCashInfoAsync(CashInfo cash);

        Task UpdateCashInfoAsync(CashInfo cash);

        Task<bool> CashInfoExistsAsync(Guid id);

        Task SaveChangesAsync();
    }
}