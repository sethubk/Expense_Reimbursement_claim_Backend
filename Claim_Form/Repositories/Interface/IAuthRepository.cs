using Claim_Form.Entities;

namespace Claim_Form.Repositories.Interface
{
    public interface IAuthRepository
    {
        Task<Employee?> GetEmployeeAsync(string Empcode);
    }
}
