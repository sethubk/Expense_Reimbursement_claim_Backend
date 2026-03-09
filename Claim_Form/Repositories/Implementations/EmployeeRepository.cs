using Claim_Form.Data;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Claim_Form.Repositories.Implementations
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context=context;
        }

        public async Task<Employee?> GetEmployeeAsync(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(e=>e.Email==email);
        }
        public async Task<Employee> GetEmployee(string EmpCode)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.EmpCode == EmpCode);
        }

        public async Task<Employee?> GetEmployeewithClaim(string EmpCode)
        {
            return await _context.Employees.Include(e => e.RecentClaims).FirstOrDefaultAsync(e => e.EmpCode == EmpCode);
        }
    }
}
