using Claim_Form.Dtos;

namespace Claim_Form.Services.Interface
{
    public interface IExpenseService
    {
        Task<ExpenseDto> AddExpense(Guid claimid, ExpenseDto dto);
        Task<IEnumerable<ExpenseDto>> GetExpense();
        Task<ExpenseDto?> GetExpenseAsync(Guid id);
        Task<ExpenseDto> UpdateExpense(Guid id, ExpenseDto dto);
        Task<ExpenseDto> DeleteExpense(Guid id);
    }
}
