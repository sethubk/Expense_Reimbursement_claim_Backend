using Claim_Form.Data;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Claim_Form.Repositories.Implementations
{
    /// <summary>
    /// Repository implementation for international expense data operations.
    /// </summary>
    public class InternationalRepository : IInternationalRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternationalRepository"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public InternationalRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds multiple international expense entries in a single operation.
        /// </summary>
        /// <param name="expenses">International expense entities.</param>
        public async Task CreateInternationalExpenseAsync(IEnumerable<International> expenses)
        {
            await _context.Internationals.AddRangeAsync(expenses);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves an international expense by its identifier.
        /// </summary>
        /// <param name="id">International expense identifier.</param>
        /// <returns>International expense if found; otherwise null.</returns>
        public async Task<International?> GetByIdAsync(Guid id)
        {
            return await _context.Internationals
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Updates an existing international expense entry.
        /// </summary>
        /// <param name="expense">International expense entity.</param>
        public async Task UpdateAsync(International expense)
        {
            _context.Internationals.Update(expense);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an international expense by its identifier.
        /// </summary>
        /// <param name="id">International expense identifier.</param>
        public async Task DeleteAsync(Guid id)
        {
            var expense = await _context.Internationals.FindAsync(id);

            if (expense == null)
                return;

            _context.Internationals.Remove(expense);
            await _context.SaveChangesAsync();
        }
    }
}