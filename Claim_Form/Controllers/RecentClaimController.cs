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
        /// <param name="id">Claim identifier.</param>
        /// <returns>Claim details.</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(RecentClaimResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClaim([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Claim ID cannot be empty.");

            try
            {
                var claim = await _recentClaimService.GetClaimAsync(id);

                if (claim == null)
                    return NotFound($"No claim found with ID '{id}'.");

                return Ok(claim);
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
        /// <param name="dto">Claim creation details.</param>
        /// <param name="empCode">Employee code.</param>
        /// <returns>Created claim.</returns>
        [HttpPost("{empCode}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateClaim(
            [FromBody] RecentClaimDto dto,
            [FromRoute] string empCode)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            if (string.IsNullOrWhiteSpace(empCode))
                return BadRequest("Employee code is required.");

            try
            {
                var created = await _recentClaimService.CreateClaimAsync(dto, empCode);

                if (created == null)
                    return NotFound($"Employee with code '{empCode}' not found.");

                return Ok(created);
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
        /// <param name="dto">Updated claim details.</param>
        /// <param name="empCode">Employee code.</param>
        /// <param name="id">Claim identifier.</param>
        /// <returns>Updated claim.</returns>
        [HttpPut("{empCode}/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClaim(
            [FromBody] UpdateClaimDto dto,
            [FromRoute] string empCode,
            [FromRoute] Guid id)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            if (string.IsNullOrWhiteSpace(empCode))
                return BadRequest("Employee code is required.");

            if (id == Guid.Empty)
                return BadRequest("Invalid claim id.");

            try
            {
                var result = await _recentClaimService.UpdateClaimAsync(dto, empCode, id);

                if (result == null)
                    return NotFound($"Claim '{id}' not found.");

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
        /// Retrieves all claims for a specific employee using employee identifier.
        /// </summary>
        /// <param name="empId">Employee identifier.</param>
        /// <returns>Employee claims.</returns>
        [HttpGet("{empId}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClaimByEmployeeId([FromRoute] Guid empId)
        {
            if (empId == Guid.Empty)
                return BadRequest("Invalid employee id.");

            try
            {
                var result = await _recentClaimService.GetClaimByEmpIDAsync(empId);

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
        /// Retrieves all claims for a specific employee using employee code.
        /// </summary>
        /// <param name="empCode">Employee code.</param>
        /// <returns>Employee claims.</returns>
        [HttpGet("{empCode}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClaimByEmpCode([FromRoute] string empCode)
        {
            if (string.IsNullOrWhiteSpace(empCode))
                return BadRequest("Employee code is required.");

            try
            {
                var result = await _recentClaimService.GetClaimByEmpCodeAsync(empCode);

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
        /// <param name="empCode">Employee code.</param>
        /// <returns>No content.</returns>
        [HttpDelete("draft/{empCode}")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDraft([FromRoute] string empCode)
        {
            if (string.IsNullOrWhiteSpace(empCode))
                return BadRequest("Employee code is required.");

            try
            {
                var deleted = await _recentClaimService.DeleteDraftAsync(empCode);

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
    }
}