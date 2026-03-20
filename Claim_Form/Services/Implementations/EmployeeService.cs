using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Claim_Form.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _authRepository;
        private readonly IConfiguration _configuration;

        public EmployeeService(IConfiguration configuration, IEmployeeRepository authRepository)
        {
            _configuration = configuration;
            _authRepository = authRepository;

        }
        public async Task<EmployeeResponseDtos> GetEmployeeAsync(EmployeeLoginDtos dto)
        {
            var Employee = await _authRepository.GetEmployeeAsync(dto.Email);
            if (Employee == null)
            {

                throw new Exception("invalid Employeee code");
            }




            if (!BCrypt.Net.BCrypt.Verify(dto.Password, Employee.passwordHash))
            {
                throw new Exception("invalid password");
            }
            var token = GenerateJwtToken(Employee);




            return new EmployeeResponseDtos
            {

                EmpCode = Employee.EmpCode,
                Name = Employee.Name,
                Department = Employee.Department,
                Role = Employee.Role,
                Email = Employee.Email,
                VenderCost = Employee.VenderCost,
                CostCenter = Employee.CostCenter
            };

        }
        private string GenerateJwtToken(Employee employee)
        {
            //var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var key = new SymmetricSecurityKey(
          Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
      );
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, employee.Name),
                    new Claim("EmpCode", employee.EmpCode),
                    new Claim("Department", employee.Department ?? ""),
                    new Claim(ClaimTypes.Role, employee.Role ?? ""),
                    new Claim(ClaimTypes.Email, employee.Email ?? ""),
                    new Claim("VenderCost", employee.VenderCost ?? ""),
                    new Claim("CostCenter", employee.CostCenter ?? "")
                };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                 claims: claims,
           expires: DateTime.Now.AddHours(2),
           signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<EmpWithClaimDto?> AllEmp(string EmpCode)
        {
            var emp = await _authRepository.GetEmployee(EmpCode);
            if (emp != null)
            {
                var emp1 = await _authRepository.GetEmployeewithClaim(EmpCode);

            }
            else
            {
                throw new Exception("emp not found");
            }

            return new EmpWithClaimDto
            {
                EmpCode = emp.EmpCode,
                Name = emp.Name,
                Email = emp.Email,
                VenderCost = emp.VenderCost,
                Department = emp.Department,
                RecentClaims = emp.RecentClaims.Select(c => new RecentClaimDto
                {
                    Type = c.Type,
                    Date = c.Date,
                    Purpose = c.Purpose,
                    Amount = c.Amount,
                    Status = c.Status,


                }).ToList()

            };
        }
    }
    
}


