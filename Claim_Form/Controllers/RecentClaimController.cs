using Claim_Form.Dtos;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Claim_Form.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecentClaimController : ControllerBase
    {
        private readonly IRecentClaimService _recentClaimService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentClaimController"/>.
        /// </summary>
        /// <param name="recentClaimService">Recent claim service.</param>
        public RecentClaimController(IRecentClaimService recentClaimService)
        {
            _recentClaimService = recentClaimService;
        }
         
        /// <summary>
        /// Retrieves a single claim using its unique claim identifier.
        /// </summary>
        /// <param name="claimId">Claim identifier.</param>
        /// <returns>Claim details.</returns>
        // GET api/recentClaim/<claimId>/claim
        [HttpGet("{claimId}/claim")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(RecentClaimResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClaim([FromRoute] Guid claimId)
        {
            if (claimId == Guid.Empty)
                return BadRequest("Claim ID cannot be empty.");

            try
            {
                var result = await _recentClaimService.GetClaimAsync(claimId);

                if (result == null)
                    return NotFound($"No claim found with ID '{claimId}'.");

                return Ok(result);
            }
            catch
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An error occurred while retrieving the claim.");
            }
        }

        /// <summary>
        /// Creates a new claim for a given employee.
        /// </summary>
        /// <param name="input">Claim creation details.</param>
        /// <param name="employeeCode">Employee code.</param>
        /// <returns>Created claim.</returns>
        // POST api/recentClaim/<employeeCode>/claim
        [HttpPost("{employeeCode}/claim")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateClaim(
            [FromBody] RecentClaimDto input,
            [FromRoute] string employeeCode)
        {
            if (input == null)
                return BadRequest("Request body is required.");

            if (string.IsNullOrWhiteSpace(employeeCode))
                return BadRequest("Employee code is required.");

            try
            {
                var result = await _recentClaimService.CreateClaimAsync(input, employeeCode);

                if (result == null)
                    return NotFound($"Employee with code '{employeeCode}' not found.");

                return Ok(result);
            }
            catch
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An error occurred while creating the claim.");
            }
        }

        /// <summary>
        /// Updates an existing claim for a specific employee.
        /// </summary>
        /// <param name="input">Updated claim details.</param>
        /// <param name="employeeCode">Employee code.</param>
        /// <param name="claimId">Claim identifier.</param>
        /// <returns>Updated claim.</returns>
        // PUT api/recentClaim/<employeeCode>/<claimId>/claim 
        [HttpPut("{employeeCode}/{claimId}/claim")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClaim(
            [FromBody] UpdateClaimDto input,
            [FromRoute] string employeeCode,
            [FromRoute] Guid claimId)
        {
            if (input == null)
                return BadRequest("Request body is required.");

            if (string.IsNullOrWhiteSpace(employeeCode))
                return BadRequest("Employee code is required.");

            if (claimId == Guid.Empty)
                return BadRequest("Invalid claim id.");

            try
            {
                var result = await _recentClaimService.UpdateClaimAsync(input, employeeCode, claimId);

                if (result == null)
                    return NotFound($"Claim '{claimId}' not found.");

                return Ok(result);
            }
            catch
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An error occurred while updating the claim.");
            }
        }

        /// <summary>
        /// Retrieves all claims for a specific employee using employee code.
        /// </summary>
        /// <param name="employeeCode">Employee code.</param>
        /// <returns>Employee claims.</returns>
        //  // get api/recentClaim/ID/Expense
        [HttpGet("{employeeCode}/claims")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClaimByEmpCode([FromRoute] string employeeCode)
        {
            if (string.IsNullOrWhiteSpace(employeeCode))
                return BadRequest("Employee code is required.");

            try
            {
                var result = await _recentClaimService.GetClaimByEmpCodeAsync(employeeCode);

                if (result == null)
                    return NotFound("No claims found for this employee.");

                return Ok(result);
            }
            catch
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An error occurred while retrieving claims.");
            }
        }

        /// <summary>
        /// Deletes draft claims for a specific employee.
        /// </summary>
        /// <param name="Employeecode">Employee code.</param>
        /// <returns>No content.</returns>
        ///  // delete api/recentClaim/Employeecode
        [HttpDelete("draft/{Employeecode}")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDraft([FromRoute] string Employeecode)
        {
            if (string.IsNullOrWhiteSpace(Employeecode))
                return BadRequest("Employee code is required.");

            try
            {
                var deleted = await _recentClaimService.DeleteDraftAsync(Employeecode);

                if (deleted == null)
                    return NotFound("No draft claim found for this employee.");

                return NoContent();
            }
            catch
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An error occurred while deleting the draft claim.");
            }
        }


        /// <summary>
        /// Updates an existing claim for a specific employee.
        /// </summary>
        /// <param name="input">Updated claim details.</param>
       
        /// <param name="claimId">Claim identifier.</param>
        /// <returns>Updated claim.</returns>
        // PUT api/recentClaim/<employeeCode>/<claimId>/claim 
        [HttpPut("{claimId}/claimstatus")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClaimStatus(
             [FromRoute] Guid claimId,[FromBody] ClaimStatusDto status
            
           )
        {
            if (status == null)
                return BadRequest("Request body is required.");


            if (claimId == Guid.Empty)
                return BadRequest("Invalid claim id.");

            try
            {
                var result = await _recentClaimService.UpdateClaimStatusAsync(status, claimId);

                if (result == null)
                    return NotFound($"Claim '{claimId}' not found.");

                return Ok(result);
            }
            catch
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An error occurred while updating the claim.");
            }
        }
    }
}