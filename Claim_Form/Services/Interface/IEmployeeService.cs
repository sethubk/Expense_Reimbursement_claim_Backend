using Claim_Form.Dtos;

namespace Claim_Form.Services.Interface
{
    /// <summary>
    /// Defines employee-related business operations.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Authenticates an employee using login credentials.
        /// </summary>
        /// <param name="dto">Employee login details.</param>
        /// <returns>Employee response details.</returns>
        Task<EmployeeResponseDto> GetEmployeeAsync(EmployeeLoginDto dto);

        /// <summary>
        /// Retrieves employee details along with associated claims.
        /// </summary>
        /// <param name="empCode">Employee code.</param>
        /// <returns>Employee with claims if found; otherwise null.</returns>
        Task<EmpWithClaimDto?> GetEmployeeWithClaimsAsync(string empCode);
    }
}
