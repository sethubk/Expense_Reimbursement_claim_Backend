using Claim_Form.Dtos;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Claim_Form.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternationalTravel : ControllerBase
    {
        private readonly IInternationalTravelService _internationalTravelService;

        public InternationalTravel(IInternationalTravelService internationalTravelService)
        {
            _internationalTravelService = internationalTravelService;
        }


        //// <summary>
        //// Creates a new TravelDetails record linked to a ClaimID
        //// </summary>
        //// <param name="claimId">cretae the travel details , like start and ens date od travel.</param>
        [HttpPost("{claimId}")]
        [SwaggerResponse(200, "Success")]
        //[SwaggerResponse(400, "Bad Request")]
        //[SwaggerResponse(404, "Not Found")]
        //[SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> AddTravelDetails(Guid claimId,[FromBody] TravelDetailsDtos travelDetailsDtos)
        {

            if(travelDetailsDtos == null)
            return BadRequest("Request body cannot be empty.");

            try
            {
                var created= await _internationalTravelService.AddTravelDetails(claimId, travelDetailsDtos);

                return Ok(created);

            }
            catch (Exception ex)
            {
               return NotFound($"No claim found with ID = {claimId}");
            }

        }


        //// <summary>
        //// get travel details TravelDetails record linked to a ClaimID
        //// </summary>
        //// <param name="claimId" get the travel details , like start and ens date od travel.</param>
        [HttpGet("{claimId}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetTravelByClaimId(Guid claimId)
        {
            var result = await _internationalTravelService.GetTravelByClaimId(claimId);

            if (result == null)
            {
                return NotFound(new { message = "No travel details found for this claim." });
            }

            return Ok(result);
        }


    }
}
