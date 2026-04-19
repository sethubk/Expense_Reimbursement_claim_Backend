using AutoMapper;
using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Claim_Form.Services.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Claim_Form.Services.Implementations
{
    /// <summary>
    /// Service implementation for employee-related operations.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        public EmployeeService(
            IConfiguration configuration,
            IEmployeeRepository employeeRepository,IMapper mapper)
        {
            _configuration = configuration;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Authenticates an employee and returns employee details.
        /// </summary>
        /// <param name="dto">Employee login credentials.</param>
        /// <returns>Employee response DTO.</returns>
        public async Task<EmployeeResponseDto> GetEmployeeAsync(EmployeeLoginDto dto)
        {
            var employee = await _employeeRepository
                .GetEmployeeByEmailAsync(dto.Email);

            if (employee == null)
                throw new InvalidOperationException("Invalid employee email.");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, employee.PasswordHash))
                throw new InvalidOperationException("Invalid password.");

            var token = GenerateJwtToken(employee);

            //return new EmployeeResponseDto
            //{
            //    EmpCode = employee.EmpCode,
            //    Name = employee.Name,
            //    Department = employee.Department,
            //    Role = employee.Role,
            //    Email = employee.Email,
            //    VendorCost = employee.VendorCost,
            //    CostCenter = employee.CostCenter
            //};
           var emp= _mapper.Map<EmployeeResponseDto>(employee);
            emp.Token = token;
            return emp;

        }

        /// <summary>
        /// Retrieves employee details along with their claims.
        /// </summary>
        /// <param name="empCode">Employee code.</param>
        /// <returns>Employee with claims DTO.</returns>
        public async Task<EmpWithClaimDto?> GetEmployeeWithClaimsAsync(string empCode)
        {
            var employee = await _employeeRepository
                .GetEmployeeWithClaimsAsync(empCode);

            if (employee == null)
                return null;

            //return new EmpWithClaimDto
            //{
            //    EmpCode = employee.EmpCode,
            //    Name = employee.Name,
            //    Email = employee.Email,
            //    Department = employee.Department,
            //    VendorCost = employee.VendorCost,
            //    RecentClaims = employee.RecentClaims
            //        .Select(c => new RecentClaimDto
            //        {
            //            Type = c.Type,
            //            Date = c.Date,
            //            Purpose = c.Purpose,
            //            Amount = c.Amount,
            //            Status = c.Status
            //        })
            //        .ToList()
            //};
            return _mapper.Map<EmpWithClaimDto>(employee);
        }

        /// <summary>
        /// Generates a JWT token for an authenticated employee.
        /// </summary>
        private string GenerateJwtToken(Employee employee)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, employee.Name),
                new Claim(ClaimTypes.Email, employee.Email),
                new Claim(ClaimTypes.Role, employee.Role),
                new Claim("EmpCode", employee.EmpCode),
                new Claim("Department", employee.Department),
                new Claim("VendorCost", employee.VendorCost),
                new Claim("CostCenter", employee.CostCenter)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}