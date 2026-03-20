using Claim_Form.Dtos;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Claim_Form.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecentClaimController : ControllerBase
    {
        private readonly IRecentClaimService _recentClaimService;

        public RecentClaimController(IRecentClaimService recentClaimService)
        {
            _recentClaimService = recentClaimService;
        }



        /// <summary>
        /// get the Claim using the claimID id=claimid
        /// </summary>
        /// <param name="id">Login payload containing EmpEmail and Password.</param>
        /// <returns>Claims.</returns>
        ///
        [HttpGet("id:Guid")]
        [SwaggerResponse(200, "Success", typeof(RecentClaimResponseDto))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetClaim(Guid id)
        {

            try
            {
                // Validate input
                if (id == Guid.Empty)
                    return BadRequest("Claim ID cannot be empty.");

                // Call service
                var claim = await _recentClaimService.GetClaim(id);

                if (claim == null)
                    return NotFound($"No claim found with ID: {id}");

                return Ok(claim);
            }
            catch (KeyNotFoundException ex)
            {
                // For invalid parameters thrown in service
                return NotFound(ex.Message);
            }

        }
        /// <summary>
        /// create a Claim using the claimID
        /// </summary>
        /// <param name="Empcode">Create the Claim using the Empcode , this empcode is used to find the Employee</param>
        /// <returns>Created Claims.</returns>
        ///
        [HttpPost("{Empcode}")]
        [SwaggerResponse(200, "Success")]
        //[SwaggerResponse(400, "Bad Request")]
        //[SwaggerResponse(404, "Not Found")]
        //[SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> CreateClaimAsync([FromBody] RecentClaimDto dto, [FromRoute] string Empcode)
        {
            if (dto == null) return BadRequest("Request body is required.");
            if (string.IsNullOrWhiteSpace(Empcode)) return BadRequest("Empcode is required.");

            try
            {
                var created = await _recentClaimService.CreateClaimAsync(dto, Empcode);
                if (created is null) return BadRequest("Failed to create claim.");
                return Ok(created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
           
        }

        /// <summary>
        /// Updates an existing claim's details such as amount or status.  
        /// This is typically used after adding or editing expenses tied to the claim.
        /// </summary>
        /// <param name="Empcode">
        /// The employee code used to identify the employee who owns the claim.
        /// </param>
        /// <param name="id">
        /// The unique identifier of the claim to be updated.
        /// </param>
        /// <returns>
        /// The updated claim details.
        /// </returns>
        [HttpPut("{Empcode}/{id:guid}")]
        [SwaggerResponse(200, "Success")]
        //[SwaggerResponse(400, "Bad Request")]
        //[SwaggerResponse(404, "Not Found")]
        //[SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> UpdateClaim([FromBody] UpdateClaimDto dto, [FromRoute] string Empcode, [FromRoute] Guid id)
        {
            if (dto == null) return BadRequest("Request body is required.");
            if (string.IsNullOrWhiteSpace(Empcode)) return BadRequest("Empcode is required.");
            if (id == Guid.Empty) return BadRequest("Invalid claim id.");

            try
            {
                var result = await _recentClaimService.UpdateClaimAsync(dto, Empcode, id);
                if (result is null) return NotFound($"Claim {id} not found.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
           
        }

        /// <summary>
        /// Retrieves all claim records associated with a specific employee.
        /// </summary>
        /// <param name="empId">
        /// The unique identifier (GUID) of the employee whose claims need to be fetched.
        /// </param>
        /// <returns>
        /// A list of claims for the specified employee if found; otherwise, an appropriate HTTP error response.
        /// </returns>
        [HttpGet("{empId:guid}")]
        [SwaggerResponse(200, "Success")]
        public async Task<IActionResult> GetClaimByEmpID([FromRoute] Guid empId)
        {
            if (empId == Guid.Empty) return BadRequest("Invalid employee id.");

            try
            {
                var result = await _recentClaimService.GetClaimByEmpID(empId);
                if (result == null) return NotFound("No claim found for this employee.");
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
        /// <summary>
        /// Retrieves all claim records associated with a specific employee.
        /// </summary>
        /// <param name="Empcode">
        /// the employee whose claims need to be fetched.
        /// </param>
        /// <returns>
        /// A list of claims for the specified employee if found; otherwise, an appropriate HTTP error response.
        /// </returns>

        [HttpGet("{Empcode}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetClaimByEmpCode([FromRoute] string Empcode)
        {
            if (string.IsNullOrWhiteSpace(Empcode)) return BadRequest("Empcode is required.");

            try
            {
                var result = await _recentClaimService.GetClaimByEmpCode(Empcode);
                if (result == null) return NotFound("No claim found for this employee.");
                return Ok(result);
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
        
        [HttpDelete("draft/{Empcode}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> DeleteDraft([FromRoute] string Empcode)
        {
            if (string.IsNullOrWhiteSpace(Empcode)) return BadRequest("Empcode is required.");

            try
            {
                var result = await _recentClaimService.DeleteDraft(Empcode);

                // If your service returns bool: use this block
                if (result is bool okBool)
                    return okBool ? NoContent() : NotFound("No draft found for this employee.");

                // If your service returns an object: use this block
                if (result == null)
                    return NotFound("No claim found for this employee.");

                return Ok(result);
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }


    }
}
