using Claim_Form.Dtos;

namespace Claim_Form.Services.Interface
{
    public interface IAuthService
    {
        Task<EmployeeResponseDtos> GetEmployeeAsync(EmployeeLoginDtos dto);
    }
}
