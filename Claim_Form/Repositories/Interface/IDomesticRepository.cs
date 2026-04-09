using Claim_Form.Entities;

namespace Claim_Form.Repositories.Interface
{
    public interface IDomesticRepository
    {
        /// <summary>
        /// Retrieves an international expense by its identifier.
        /// </summary>
        /// <param name="id">International expense identifier.</param>
        /// <returns>International expense if found; otherwise null.</returns>
        Task<Domestic?> GetByIdAsync(Guid id);

        /// <summary>
        /// Updates an existing international expense.
        /// </summary>
        /// <param name="expense">International expense entity.</param>
        Task UpdateAsync(Domestic expense);

        /// <summary>
        /// Deletes an international expense by its identifier.
        /// </summary>
        /// <param name="id">International expense identifier.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Adds multiple international expense entries in a single operation.
        /// </summary>
        /// <param name="expenses">Collection of international expense entities.</param>
        Task CreateDomesticExpenseAsync(IEnumerable<Domestic> expenses);
    }
}
