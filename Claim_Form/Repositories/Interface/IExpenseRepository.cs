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
        Task<IEnumerable<Expense>?> GetByIdAsync(Guid id);
        Task<Expense?> GetById(Guid id);

        /// <summary>
        /// Updates an existing expense entry.
        /// </summary>
        /// <param name="expense">Expense entity.</param>
        Task UpdateAsync(IEnumerable<Expense> expenses);

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

        Task SaveChangesAsync();


       
        Task UpdateAsync(Expense expense);
        Task<bool> ExistsAsync(Guid id);
    }
}