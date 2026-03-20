using Claim_Form.Data;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Claim_Form.Repositories.Implementations
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;

        public ExpenseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

        }
        public async Task<Expense> GetExpenseById(Guid id)
        {
            return await _context.Expenses.FirstOrDefaultAsync(e => e.ExpenseId == id);
        }
        public async Task UpdateExpense(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteExpense(Guid id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
                return;

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }
        public async Task CreateBulkAsync(List<Expense> expenses)
        {
            await _context.Expenses.AddRangeAsync(expenses);
            await _context.SaveChangesAsync();
        }

    }
}
