using Claim_Form.Dtos;
using Claim_Form.Services.Interface;
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
        /// <param name="Id">Claim identifier.</param>
        /// <param name="travelDetailsDto">
        /// Travel details including start date and end date.
        /// </param>
        /// <returns>Created travel details.</returns>
        [HttpPost("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Travel details created successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Claim not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error.")]
        public async Task<IActionResult> AddTravelDetails(
            [FromRoute] Guid Id,
            [FromBody] TravelDetailsDto travelDetailsDto)
        {
            if (Id == Guid.Empty)
                return BadRequest("Claim id is required.");

            if (travelDetailsDto == null)
                return BadRequest("Request body cannot be empty.");

            try
            {
                var created = await _internationalTravelService
                    .AddTravelDetailsAsync(Id, travelDetailsDto);

                if (created == null)
                    return NotFound($"No claim found with id '{Id}'.");

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
        [HttpGet("{claimId:guid}")]
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