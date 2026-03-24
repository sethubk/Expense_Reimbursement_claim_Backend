using Claim_Form.Entities;

namespace Claim_Form.Repositories.Interface
{
    public interface IInternationalRepository
    {
        Task<International> GetInternationalExpenseById(Guid id);
        Task UpdateInternationalExpense(International expense);

        Task DeleteInternationalExpense(Guid id);
        Task CreateBulkAsync(List<International> expenses);
    }
}
