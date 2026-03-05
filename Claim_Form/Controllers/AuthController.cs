using Claim_Form.Dtos;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Claim_Form.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
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
    }
}
