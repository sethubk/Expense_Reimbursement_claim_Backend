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
    public class AuthService:IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration, IAuthRepository authRepository)
        {
            _configuration = configuration;
            _authRepository = authRepository;

        }

        public async Task<object> GetEmployeeAsync(EmployeeLoginDtos dto)
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

            


            return new
            {
               
                empcode = Employee.EmpCode,
                name = Employee.Name
            };

        }
        private string GenerateJwtToken(Employee employee) {

            //var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            
            var key = new SymmetricSecurityKey(
          Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
      );
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim> { 

                new Claim(ClaimTypes.Name, employee.Name),
                new Claim("Empcode",employee.EmpCode)
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
            
    }
}
