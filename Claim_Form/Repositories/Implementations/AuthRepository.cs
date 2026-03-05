using Claim_Form.Data;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Claim_Form.Repositories.Implementations
{
    public class AuthRepository:IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context=context;
        }

        public async Task<Employee?> GetEmployeeAsync(string Empcode)
        {
            return await _context.Employees.FirstOrDefaultAsync(e=>e.EmpCode==Empcode);
        }
    }
}
