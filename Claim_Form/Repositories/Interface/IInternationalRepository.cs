using Claim_Form.Entities;

namespace Claim_Form.Repositories.Interface
{
    /// <summary>
    /// Defines data access operations for international expenses.
    /// </summary>
    public interface IInternationalRepository
    {
        /// <summary>
        /// Retrieves an international expense by its identifier.
        /// </summary>
        /// <param name="id">International expense identifier.</param>
        /// <returns>International expense if found; otherwise null.</returns>
        Task<International?> GetByIdAsync(Guid id);

        /// <summary>
        /// Updates an existing international expense.
        /// </summary>
        /// <param name="expense">International expense entity.</param>
        Task UpdateAsync(International expense);

        /// <summary>
        /// Deletes an international expense by its identifier.
        /// </summary>
        /// <param name="id">International expense identifier.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Adds multiple international expense entries in a single operation.
        /// </summary>
        /// <param name="expenses">Collection of international expense entities.</param>
        Task AddBulkAsync(IEnumerable<International> expenses);
    }
}