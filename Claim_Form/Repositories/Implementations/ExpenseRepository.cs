using Claim_Form.Data;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Claim_Form.Repositories.Implementations
{
    /// <summary>
    /// Repository implementation for expense-related data operations.
    /// </summary>
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseRepository"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public ExpenseRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new expense entry.
        /// </summary>
        /// <param name="expense">Expense entity.</param>
        public async Task AddAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves an expense by its identifier.
        /// </summary>
        /// <param name="id">Expense identifier.</param>
        /// <returns>Expense if found; otherwise null.</returns>
        public async Task<IEnumerable<Expense>?> GetByIdAsync(Guid id)
        {
            return await _context.Expenses
                .AsNoTracking()
                .Where(e => e.RecentClaimId == id).ToListAsync();
        }

        /// <summary>
        /// Updates an existing expense entry.
        /// </summary>
        /// <param name="expense">Expense entity.</param>
        public async Task UpdateAsync(IEnumerable<Expense> expenses)
        {
            _context.Expenses.UpdateRange(expenses);
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Deletes an expense by its identifier.
        /// </summary>
        /// <param name="id">Expense identifier.</param>
        public async Task DeleteAsync(Guid id)
        {

            var expenses = await _context.Expenses
                    .Where(x => x.RecentClaimId == id)
                    .ToListAsync();

            _context.Expenses.RemoveRange(expenses);
            await _context.SaveChangesAsync();

        }


        /// <summary>
        /// Adds multiple expense entries in a single operation.
        /// </summary>
        /// <param name="expenses">List of expenses.</param>
        public async Task AddExpenseAsync(IEnumerable<Expense> expenses)
        {
            await _context.Expenses.AddRangeAsync(expenses);
            await _context.SaveChangesAsync();
        }
    }
}