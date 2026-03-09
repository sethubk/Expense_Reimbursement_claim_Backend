using Claim_Form.Dtos;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


        [HttpGet("id:Guid")]
        public async Task<IActionResult> GetClaim(Guid id)
        {
            var claim = await _recentClaimService.GetClaim(id);
            return Ok(claim);
        }


        [HttpGet]
        public async Task<IActionResult> GetClaims()
        {
            return Ok(await _recentClaimService.GetClaims());
        }


        [HttpPost("create/{Empcode}")]
        public async Task<IActionResult> CreateClaimAsync([FromBody]RecentClaimDto dto, string Empcode)
        {
            var created = await _recentClaimService.CreateClaimAsync(dto, Empcode);
            return Ok(created);
        }

        [HttpPut("update/{Empcode}/{id}")]
        public async Task<IActionResult> UpdateClaim([FromBody]RecentClaimDto dto, string Empcode, Guid id)
        {
            return Ok(await _recentClaimService.UpdateClaimAsync(dto, Empcode, id));
        }
    }
}
