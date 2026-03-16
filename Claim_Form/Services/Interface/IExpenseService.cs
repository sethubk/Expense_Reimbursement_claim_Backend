using Claim_Form.Dtos;
using Claim_Form.Entities;

namespace Claim_Form.Services.Interface
{
    public interface IExpenseService
    {
        Task<ExpenseDto> AddExpense(Guid claimid, ExpenseEntryDto dto);
   
        Task<ExpenseDto?> GetExpenseAsync(Guid id);
        Task<ExpenseDto> UpdateExpense(Guid id, ExpenseDto dto);
        Task<ExpenseDto> DeleteExpense(Guid id);
        Task<IEnumerable<ExpenseDto>> CreateBulkAsync(Guid claimId, List<ExpenseEntryDto> entries);
    }
}
