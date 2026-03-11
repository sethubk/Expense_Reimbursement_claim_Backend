using Claim_Form.Entities;

namespace Claim_Form.Repositories.Interface
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetEmployeeAsync(string Empcode);
         Task<Employee> GetEmployee(string EmpCode);
        Task<Employee?> GetEmployeewithClaim(string EmpCode);
        Task<Employee> GetEmployeeById(Guid id);
    }
}
