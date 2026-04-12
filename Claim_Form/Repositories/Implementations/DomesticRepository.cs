using Claim_Form.Data;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Claim_Form.Repositories.Implementations
{
    public class DomesticRepository:IDomesticRepository
    {
        private readonly AppDbContext _context;

        public DomesticRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Adds multiple international expense entries in a single operation.
        /// </summary>
        /// <param name="expenses">International expense entities.</param>
        public async Task CreateDomesticExpenseAsync(IEnumerable<Domestic> expenses)
        {
            await _context.Domestics.AddRangeAsync(expenses);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves an international expense by its identifier.
        /// </summary>
        /// <param name="id">International expense identifier.</param>
        /// <returns>International expense if found; otherwise null.</returns>
        public async Task<Domestic?> GetByIdAsync(Guid id)
        {
            return await _context.Domestics
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Updates an existing international expense entry.
        /// </summary>
        /// <param name="expense">International expense entity.</param>
        public async Task UpdateAsync(Domestic expense)
        {
            _context.Domestics.Update(expense);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an international expense by its identifier.
        /// </summary>
        /// <param name="id">International expense identifier.</param>
        public async Task DeleteAsync(Guid id)
        {
            var expense = await _context.Domestics.FindAsync(id);

            if (expense == null)
                return;

            _context.Domestics.Remove(expense);
            await _context.SaveChangesAsync();
        }
    }

}
