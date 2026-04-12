using Claim_Form.Dtos;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Claim_Form.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomesticExpenseController : ControllerBase
    {

        private readonly IDomesticService _domesticService;
        public DomesticExpenseController(IDomesticService domesticService)
        {
            _domesticService = domesticService;
        }

        // <summary>
        /// Create a domestic expense by its identifier.
        /// </summary>
        /// <param name="ClaimId">Domestic expense identifier.</param>
        /// <returns>Domestic expense details.</returns>

        ///  // POST api/Domestic/ClaimID/Domestic
        [HttpPost("{ClaimId}/DomesticExpense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateInternationalExpense(
            [FromRoute] Guid ClaimId,
            [FromBody] List<DomesticDto> entries)
        {
            if (ClaimId == Guid.Empty)
                return BadRequest("Claim id is required.");

            if (entries == null || entries.Count == 0)
                return BadRequest("International expense entries are required.");

            try
            {
                var result = await _domesticService.CreateDomesticExpense(ClaimId, entries);

                if (result == null)
                    return NotFound($"No claim found with id '{ClaimId}'.");

                return Ok(result);
            }
            catch
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An error occurred while creating international expenses.");
            }
        }



        // <summary>
        /// Retrieves a domestic expense by its identifier.
        /// </summary>
        /// <param name="id">Domestic expense identifier.</param>
        /// <returns>Domestic expense details.</returns>
        /// GET api/domestic-expense/{id}

        [HttpGet("{claimId}/Domestic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] Guid claimId)
        {
            if (claimId == Guid.Empty)
                return BadRequest("Invalid international expense id.");

            try
            {
                var expense = await _domesticService.GetDomesticAsync(claimId);

                if (expense == null)
                    return NotFound($"No international expense found with id '{claimId}'.");

                return Ok(expense);
            }
            catch
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An error occurred while retrieving the international expense.");
            }
        }


        /// <summary>
        /// Updates an existing domestic expense.
        /// </summary>
        /// <param name="id">Domestic expense identifier.</param>
        /// <param name="input">Updated domestic expense data.</param>
        /// <returns>Updated domestic expense.</returns>
        /// PUT api/domestic-expense/{id}

        [HttpPut("{claimId}/Domestic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid cliamid,
            [FromBody] DomesticDto input)
        {
            if (cliamid == Guid.Empty)
                return BadRequest("Invalid international expense id.");

            if (input == null)
                return BadRequest("Request body is required.");

            try
            {
                var updated = await _domesticService.UpdateDomesticAsync(cliamid, input);

                if (updated == null)
                    return NotFound($"No international expense found with id '{cliamid}'.");

                return Ok(updated);
            }
            catch
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An error occurred while updating the international expense.");
            }
        }

        /// <summary>
        /// Deletes an Domestic expense by its identifier.
        /// </summary>
        /// <param name="ClaimId">Domestic expense identifier.</param>
        /// <returns>No content.</returns>
        ///  delete api/Domestic/ClaimID/Domestic
        [HttpDelete("{claimId}/Domestic")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Guid ClaimId)
        {
            if (ClaimId == Guid.Empty)
                return BadRequest("Invalid international expense id.");

            try
            {
                var deleted = await _domesticService.DeleteDomesticAsync(ClaimId);

                if (deleted == null)
                    return NotFound($"No international expense found with id '{ClaimId}'.");

                return NoContent();
            }
            catch
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An error occurred while deleting the international expense.");
            }
        }
    }

}
