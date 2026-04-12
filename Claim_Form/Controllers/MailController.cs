using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Claim_Form.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        /// <summary>
        /// Sends an email notification for an employee's expense claim.
        /// </summary>
       
        /// <param name="ClaimId">The unique identifier of the claim for which the mail needs to be sent.</param>

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Mail sent successfully.", typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input parameters.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Employee or claim not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error.")]
        public async Task<IActionResult> SendMail(string Empcode, Guid ClaimId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Empcode))
                    return BadRequest("Employee code is required.");

                if (ClaimId == Guid.Empty)
                    return BadRequest("Claim ID is required.");

                var result = await _mailService.SendMailByEmpCode(Empcode, ClaimId);

                return Ok(new
                {
                    success = true,
                    message = result
                });
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (FormatException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid email format: " + ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Something went wrong while sending email.",
                    error = ex.Message
                });
            }


        }

        [HttpPost("AdminAction")]
        [SwaggerResponse(StatusCodes.Status200OK, "Mail sent successfully.", typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input parameters.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Employee or claim not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error.")]
        public async Task<IActionResult> AdminAction(string Empcode, Guid ClaimId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Empcode))
                    return BadRequest("Employee code is required.");

                if (ClaimId == Guid.Empty)
                    return BadRequest("Claim ID is required.");

                var result = await _mailService.AdminAction(Empcode, ClaimId);

                return Ok(new
                {
                    success = true,
                    message = result
                });
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (FormatException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid email format: " + ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Something went wrong while sending email.",
                    error = ex.Message
                });
            }


        }
    }
}
