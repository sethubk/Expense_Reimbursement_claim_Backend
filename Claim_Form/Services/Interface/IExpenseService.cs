using Claim_Form.Dtos;

namespace Claim_Form.Services.Interface
{
    /// <summary>
    /// Defines business operations related to expenses.
    /// </summary>
    public interface IExpenseService
    {
        /// <summary>
        /// Creates multiple expense entries for a given claim.
        /// </summary>
        /// <param name="claimId">Claim identifier.</param>
        /// <param name="entries">Expense entry details.</param>
        /// <returns>Created expenses.</returns>
        Task<IEnumerable<ExpenseDto>> CreateExpenseAsync(
            Guid claimId,
            List<ExpenseEntryDto> entries);

        /// <summary>
        /// Retrieves an expense by its identifier.
        /// </summary>
        /// <param name="id">Expense identifier.</param>
        /// <returns>Expense details if found; otherwise null.</returns>
        Task<List<ExpenseDto?>> GetExpenseAsync(Guid id);

        /// <summary>
        /// Updates an existing expense.
        /// </summary>
        /// <param name="id">Expense identifier.</param>
        /// <param name="dto">Updated expense details.</param>
        /// <returns>Updated expense details if found; otherwise null.</returns>
        Task<bool> UpdateExpenseAsync(Guid claimId, List<ExpenseDto> expenses);

        /// <summary>
        /// Deletes an expense by its identifier.
        /// </summary>
        /// <param name="id">Expense identifier.</param>
        /// <returns>True if deleted; false if not found.</returns>
        Task<bool> DeleteExpenseAsync(Guid id);
    }
}