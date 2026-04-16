using Claim_Form.Dtos;

namespace Claim_Form.Services.Interface
{
    /// <summary>
    /// Defines business operations for international expense handling.
    /// </summary>
    public interface IInternationalServices
    {
        /// <summary>
        /// Creates multiple international expense entries for a given claim.
        /// </summary>
        /// <param name="claimId">Claim identifier.</param>
        /// <param name="entries">International expense details.</param>
        /// <returns>Created international expenses.</returns>
        Task<IEnumerable<InternationalDto>> CreateInternationalExpense(
            Guid claimId,
            List<InternationalDto> entries);

        /// <summary>
        /// Retrieves an international expense by its identifier.
        /// </summary>
        /// <param name="id">International expense identifier.</param>
        /// <returns>International expense if found; otherwise null.</returns>
        Task<List<InternationalDto?>> GetInternationalAsync(Guid claimId);

        /// <summary>
        /// Updates an existing international expense.
        /// </summary>
        /// <param name="id">International expense identifier.</param>
        /// <param name="dto">Updated international expense data.</param>
        /// <returns>Updated international expense if found; otherwise null.</returns>
        Task<InternationalDto?> UpdateInternationalAsync(Guid id, InternationalDto dto);

        /// <summary>
        /// Deletes an international expense by its identifier.
        /// </summary>
        /// <param name="id">International expense identifier.</param>
        /// <returns>True if deleted; false if not found.</returns>
        Task<bool> DeleteInternationalAsync(Guid id);
    }
}