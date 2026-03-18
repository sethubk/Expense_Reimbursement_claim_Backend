using Claim_Form.Dtos;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


        /// <summary>
        /// Creates a new TravelDetails record linked to a ClaimID
        /// </summary>
        [HttpPost("{claimId:guid}")]
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

    }
}
