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

        [HttpPost("create/{Empcode}")]
        public async Task<IActionResult> CreateClaimAsync(RecentClaimDto dto, string Empcode)
        {
            var created = await _recentClaimService.CreateClaimAsync(dto, Empcode);
            return Ok(created);
        }

        [HttpPut("update/{Empcode}/{id}")]
        public async Task<IActionResult> UpdateClaim([FromBody] UpdateClaimDto dto, string Empcode, Guid id)
        {
           var result= await _recentClaimService.UpdateClaimAsync(dto, Empcode, id);
            return Ok(result);
        }


        [HttpGet("claims/{empId:guid}")]
        public async Task<IActionResult> GetClaimByEmpID(Guid empId)
        {
            var result = await _recentClaimService.GetClaimByEmpID(empId);

            if (result == null)
                return NotFound("No claim found for this employee.");

            return Ok(result);
        }
        [HttpGet("claimBycode/{Empcode}")]
        public async Task<IActionResult> GetClaimByEmpCode(string Empcode)
        {
            var result = await _recentClaimService.GetClaimByEmpCode(Empcode);

            if (result == null)
                return NotFound("No claim found for this employee.");

            return Ok(result);
        }
        [HttpDelete("draft/{Empcode}")]

        public async Task<IActionResult>DeleteDraft(string Empcode)
        {
            var result = await _recentClaimService.DeleteDraft(Empcode);

            if (result == null)
                return NotFound("No claim found for this employee.");

            return Ok(result);
        }

    }
}
