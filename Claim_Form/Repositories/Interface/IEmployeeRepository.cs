using Claim_Form.Entities;

namespace Claim_Form.Repositories.Interface
{
    /// <summary>
    /// Defines employee-related data access operations.
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Retrieves an employee by email address.
        /// </summary>
        /// <param name="email">Employee email address.</param>
        /// <returns>Employee if found; otherwise null.</returns>
        Task<Employee?> GetEmployeeByEmailAsync(string email);

        /// <summary>
        /// Retrieves an employee by employee code.
        /// </summary>
        /// <param name="empCode">Employee code.</param>
        /// <returns>Employee if found; otherwise null.</returns>
        Task<Employee?> GetEmployeeByCodeAsync(string empCode);

        /// <summary>
        /// Retrieves an employee by unique identifier.
        /// </summary>
        /// <param name="id">Employee identifier.</param>
        /// <returns>Employee if found; otherwise null.</returns>
        Task<Employee?> GetEmployeeByIdAsync(Guid id);

        /// <summary>
        /// Retrieves an employee along with their associated claims.
        /// </summary>
        /// <param name="empCode">Employee code.</param>
        /// <returns>Employee with claims if found; otherwise null.</returns>
        Task<Employee?> GetEmployeeWithClaimsAsync(string empCode);
    }
}