using Claim_Form.Data;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Claim_Form.Repositories.Implementations
{
    /// <summary>
    /// Repository implementation for employee-related database operations.
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves an employee by email address.
        /// </summary>
        /// <param name="email">Employee email address.</param>
        /// <returns>Employee if found; otherwise null.</returns>
        public async Task<Employee?> GetEmployeeByEmailAsync(string email)
        {
            return await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        /// <summary>
        /// Retrieves an employee by employee code.
        /// </summary>
        /// <param name="empCode">Employee code.</param>
        /// <returns>Employee if found; otherwise null.</returns>
        public async Task<Employee?> GetEmployeeByCodeAsync(string empCode)
        {
            return await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmpCode == empCode);
        }

        /// <summary>
        /// Retrieves an employee by unique identifier.
        /// </summary>
        /// <param name="id">Employee identifier.</param>
        /// <returns>Employee if found; otherwise null.</returns>
        public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
        {
            return await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Retrieves an employee along with their associated claims.
        /// </summary>
        /// <param name="empCode">Employee code.</param>
        /// <returns>Employee with claims if found; otherwise null.</returns>
        public async Task<Employee?> GetEmployeeWithClaimsAsync(string empCode)
        {
            return await _context.Employees
                .Include(e => e.RecentClaims)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmpCode == empCode);
        }
    }
}