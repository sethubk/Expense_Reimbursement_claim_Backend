using Claim_Form.Entities;

namespace Claim_Form.Repositories.Interface
{
    public interface IExpenseRepository
    {
        Task AddExpense(Expense expense);
        Task<Expense> GetExpenseById(Guid id);
        Task UpdateExpense(Expense expense);
    
        Task DeleteExpense(Guid id);
    }
}
