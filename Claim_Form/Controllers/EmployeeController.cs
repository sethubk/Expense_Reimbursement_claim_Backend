using Claim_Form.Data;
using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Reflection.Emit;



namespace Claim_Form.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        private readonly AppDbContext _context;

        public EmployeeController(IEmployeeService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }


        /// <summary>
        /// Logs an employee in using email and password and returns a JWT access token.
        /// </summary>
        /// <param name="dto">Login payload containing EmpEmail and Password.</param>
        /// <returns>JWT token and basic user information on success.</returns>
        /// <remarks>
        /// On success returns a short-lived JWT access token. Consider using HTTPS and secure storage.
        /// </remarks>
        [HttpPost("Login")]
        [SwaggerResponse(200, "Success", typeof(EmployeeResponseDtos))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
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

        ///// <summary>
        ///// Logs an employee in using email and password and returns a JWT access token.
        ///// </summary>
        ///// <param name="dto">Login payload containing EmpEmail and Password.</param>
        ///// <returns>JWT token and basic user information on success.</returns>
        ///// <remarks>
        ///// On success returns a short-lived JWT access token. Consider using HTTPS and secure storage.
        ///// </remarks>
        //[HttpPost("add")]
        //[SwaggerResponse(200, "Success")]
        //[SwaggerResponse(400, "Bad Request")]
        //[SwaggerResponse(404, "Not Found")]
        //[SwaggerResponse(500, "Internal Server Error")]
        //public IActionResult AddEmployee()
        //{
        //    var emp = new Employee
        //    {
        //        EmpCode = "EMP002",
        //        passwordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
        //        Name = "prabhu",
        //        Department = "Mtl",
        //        Role = "Admin",
        //        Email = "prabhu@gmail.com",
        //        VenderCost ="ven1234",
        //        CostCenter = "CC1023"
        //    };

        //    _context.Employees.Add(emp);
        //    _context.SaveChanges();

        //    return Ok("Employee Added Successfully");
        //}


        ///// <summary>
        ///// Get the Employee Details using from Empcode.
        ///// </summary>
        ///// <param name="EmpCode">Login payload containing EmpEmail and Password.</param>
        ///// <returns>JWT token and basic user information on success.</returns>
        ///// <remarks>
        ///// On success returns a short-lived JWT access token. Consider using HTTPS and secure storage.
        ///// </remarks>
        [HttpGet("{EmpCode}")]
        [SwaggerResponse(200, "Success")]
        //[SwaggerResponse(400, "Bad Request")]
        //[SwaggerResponse(404, "Not Found")]
        //[SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetEmployeeClaims(string EmpCode)
        {

            if (string.IsNullOrWhiteSpace(EmpCode))
                return BadRequest("Employee code is required.");

            try
            {
                var result = await _service.AllEmp(EmpCode);

                if (result is null )
                    return NotFound($"No claims found for employee code '{EmpCode}'.");

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                throw;

            }
        }
    }
}


