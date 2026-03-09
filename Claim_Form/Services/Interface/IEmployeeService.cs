using Claim_Form.Dtos;

namespace Claim_Form.Services.Interface
{
    public interface IEmployeeService
    {
        Task<EmployeeResponseDtos> GetEmployeeAsync(EmployeeLoginDtos dto);
        Task<EmpWithClaimDto?> AllEmp(string EmpCode);
        
    }
}
