using Claim_Form.Entities;

namespace Claim_Form.Repositories.Interface
{
    /// <summary>
    /// Defines expense-related data access operations.
    /// </summary>
    public interface IExpenseRepository
    {
        /// <summary>
        /// Adds a new expense entry.
        /// </summary>
        /// <param name="expense">Expense entity.</param>
        Task AddAsync(Expense expense);

        /// <summary>
        /// Retrieves an expense by its identifier.
        /// </summary>
        /// <param name="id">Expense identifier.</param>
        /// <returns>Expense if found; otherwise null.</returns>
        Task<Expense?> GetByIdAsync(Guid id);

        /// <summary>
        /// Updates an existing expense entry.
        /// </summary>
        /// <param name="expense">Expense entity.</param>
        Task UpdateAsync(Expense expense);

        /// <summary>
        /// Deletes an expense by its identifier.
        /// </summary>
        /// <param name="id">Expense identifier.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Adds multiple expense entries in a single operation.
        /// </summary>
        /// <param name="expenses">Collection of expense entities.</param>
        Task AddExpenseAsync(IEnumerable<Expense> expenses);
    }
}