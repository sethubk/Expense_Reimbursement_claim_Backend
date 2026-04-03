using Claim_Form.Dtos;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Claim_Form.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="employeeService">Employee service dependency.</param>
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Authenticates an employee using email and password.
        /// </summary>
        /// <param name="dto">Login payload containing employee email and password.</param>
        /// <returns>
        /// Returns employee details along with JWT token if authentication is successful.
        /// </returns>
        /// <response code="200">Login successful.</response>
        /// <response code="400">Invalid request payload.</response>
        /// <response code="401">Invalid email or password.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [SwaggerResponse(200, "Success", typeof(EmployeeResponseDto))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> Login([FromBody] EmployeeLoginDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Login details are required.");
            }

            try
            {
                var response = await _employeeService.GetEmployeeAsync(dto);

                if (response == null)
                {
                    return Unauthorized("Invalid email or password.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Ideally log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while processing the login request.");
            }
        }

        /// <summary>
        /// Retrieves all claims for a specific employee using employee code.
        /// </summary>
        /// <param name="empCode">Unique employee code.</param>
        /// <returns>List of claims associated with the employee.</returns>
        /// <response code="200">Claims retrieved successfully.</response>
        /// <response code="400">Employee code is invalid.</response>
        /// <response code="404">No claims found for the given employee code.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{empCode}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetEmployeeClaims(string empCode)
        {
            if (string.IsNullOrWhiteSpace(empCode))
            {
                return BadRequest("Employee code is required.");
            }

            try
            {
                var claims = await _employeeService.GetEmployeeWithClaimsAsync(empCode);

                if (claims == null)
                {
                    return NotFound($"No claims found for employee code '{empCode}'.");
                }

                return Ok(claims);
            }
            catch (Exception ex)
            {
                // Ideally log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while retrieving employee claims.");
            }
        }
    }
}