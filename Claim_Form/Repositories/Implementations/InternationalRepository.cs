using Claim_Form.Data;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Claim_Form.Repositories.Implementations
{
    public class InternationalRepository:IInternationalRepository
    {
        private readonly AppDbContext _context;

        public InternationalRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateBulkAsync(List<International> expenses)
        {
            await _context.Internationals.AddRangeAsync(expenses);
            await _context.SaveChangesAsync();
        }
        public async Task<International> GetInternationalExpenseById(Guid id)
        {
            return await _context.Internationals.FirstOrDefaultAsync(e => e.InternationalId == id);
        }
        public async Task UpdateInternationalExpense(International expense)
        {
            _context.Internationals.Update(expense);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteInternationalExpense(Guid id)
        {
            var expense = await _context.Internationals.FindAsync(id);

            if (expense == null)
                return;

            _context.Internationals.Remove(expense);
            await _context.SaveChangesAsync();
        }
    }
}
