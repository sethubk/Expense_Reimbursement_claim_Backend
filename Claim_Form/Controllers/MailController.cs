using Claim_Form.Dtos;
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
        public async Task<IActionResult> SendMail([FromBody] MailRequestDto request)
        {
            try
            {

                {
                    if (string.IsNullOrWhiteSpace(request.Empcode))
                        return BadRequest("Employee code required");

                    if (request.ClaimId == Guid.Empty)
                        return BadRequest("ClaimId required");
                    var result = await _mailService.SendMailByEmpCode(
        request.Empcode,
        request.ClaimId,
        request.ImageBase64
    );

                    return Ok(new
                    {
                        success = true,
                        message = result
                    });
                }
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
