using Claim_Form.Data;
using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Claim_Form.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly AppDbContext _context;

        public AuthController(IAuthService service,AppDbContext context)
        {
            _service = service;
            _context=context;
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login(EmployeeLoginDtos dto)
        {
            if (dto == null)
            {
                return BadRequest(
                    $"Error in {nameof(Login)}");
            }
            try
            {
                var result = await _service.GetEmployeeAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
return BadRequest(ex.Message);
            }
        }
        [HttpPost("add")]
        public IActionResult AddEmployee()
        {
            var emp = new Employee
            {
                EmpCode = "EMP002",
                passwordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Name = "prabhu",
                Department = "Mtl",
                Role = "Admin",
                Email = "prabhu@gmail.com",
                VenderCost ="ven1234",
                CostCenter = "CC1023"
            };

            _context.Employees.Add(emp);
            _context.SaveChanges();

            return Ok("Employee Added Successfully");
        }
    }
}
