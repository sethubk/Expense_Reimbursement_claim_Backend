using Claim_Form.Dtos;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Claim_Form.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InternationalTravelController : ControllerBase
    {
        private readonly IInternationalTravelService _internationalTravelService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternationalTravelController"/>.
        /// </summary>
        /// <param name="internationalTravelService">International travel service.</param>
        public InternationalTravelController(
            IInternationalTravelService internationalTravelService)
        {
            _internationalTravelService = internationalTravelService;
        }

        /// <summary>
        /// Creates travel details linked to a specific claim.
        /// </summary>
        /// <param name="claimId">Claim identifier.</param>
        /// <param name="input">
        /// Travel details including start date and end date.
        /// </param>
        /// <returns>Created travel details.</returns>
        ///  POST api/InternationalTravel/ClaimID/internationalTravel
        [Authorize]
        [HttpPost("{claimId}/InternationalTravel")]
        [SwaggerResponse(StatusCodes.Status200OK, "Travel details created successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Claim not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error.")]
        public async Task<IActionResult> AddTravelDetails(
            [FromRoute] Guid claimId,
            [FromBody] TravelDetailsDto input)
        {
            if (claimId == Guid.Empty)
                return BadRequest("Claim id is required.");

            if (input == null)
                return BadRequest("Request body cannot be empty.");

            try
            {
                var created = await _internationalTravelService
                    .AddTravelDetailsAsync(claimId, input);

                if (created == null)
                    return NotFound($"No claim found with id '{claimId}'.");

                return Ok(created);
            }
            catch
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An error occurred while creating travel details.");
            }
        }

        /// <summary>
        /// Retrieves travel details linked to a specific claim.
        /// </summary>
        /// <param name="claimId">Claim identifier.</param>
        /// <returns>Travel details associated with the claim.</returns>
        ///  get api/Internationaltrvel/ClaimID/internationaltrvel
        [HttpGet("{claimId}/internationalTravel")]
        [SwaggerResponse(StatusCodes.Status200OK, "Travel details retrieved successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid claim id.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Travel details not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error.")]
        public async Task<IActionResult> GetTravelByClaimId(
            [FromRoute] Guid claimId)
        {
            if (claimId == Guid.Empty)
                return BadRequest("Claim id is required.");

            try
            {
                var result = await _internationalTravelService
                    .GetTravelByClaimIdAsync(claimId);

                if (result == null)
                {
                    return NotFound("No travel details found for this claim.");
                }

                return Ok(result);
            }
            catch
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An error occurred while retrieving travel details.");
            }
        }
    }
}