using Claim_Form.Dtos;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Claim_Form.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InternationalExpenseController : ControllerBase
    {
        private readonly IInternationalServices _internationalService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternationalExpenseController"/>.
        /// </summary>
        /// <param name="internationalService">International expense service.</param>
        public InternationalExpenseController(IInternationalServices internationalService)
        {
            _internationalService = internationalService;
        }

        /// <summary>
        /// Creates multiple international expenses for a single claim.
        /// </summary>
        /// <param name="ClaimId">Claim identifier.</param>
        /// <param name="entries">List of international expense entries.</param>
        /// <returns>Created international expenses.</returns>
        ///  // POST api/International/ClaimID/international
        [HttpPost("{ClaimId}/InternationalExpense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateInternationalExpense(
            [FromRoute] Guid ClaimId,
            [FromBody] List<InternationalDto> entries)
        {
            if (ClaimId == Guid.Empty)
                return BadRequest("Claim id is required.");

            if (entries == null || entries.Count == 0)
                return BadRequest("International expense entries are required.");

            try
            {
                var result = await _internationalService.CreateInternationalExpense(ClaimId, entries);

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

        /// <summary>
        /// Retrieves an international expense by its identifier.
        /// </summary>
        /// <param name="ClaimID">International expense identifier.</param>
        /// <returns>International expense details.</returns>
        ///  Get api/International/ClaimID/international
        [HttpGet("{ClaimID}/international")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] Guid ClaimID)
        {
            if (ClaimID == Guid.Empty)
                return BadRequest("Invalid international expense id.");

            try
            {
                var expense = await _internationalService.GetInternationalAsync(ClaimID);

                if (expense == null)
                    return NotFound($"No international expense found with id '{ClaimID}'.");

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
        /// Updates an existing international expense.
        /// </summary>
        /// <param name="internationalId">International expense identifier.</param>
        /// <param name="input">Updated international expense data.</param>
        /// <returns>Updated international expense.</returns>
        ///  Put api/International/ClaimID/international
        [HttpPut("{internationalId}/international")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid internationalId,
            [FromBody] InternationalDto input)
        {
            if (internationalId == Guid.Empty)
                return BadRequest("Invalid international expense id.");

            if (input == null)
                return BadRequest("Request body is required.");

            try
            {
                var updated = await _internationalService.UpdateInternationalAsync(internationalId, input);

                if (updated == null)
                    return NotFound($"No international expense found with id '{internationalId}'.");

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
        /// Deletes an international expense by its identifier.
        /// </summary>
        /// <param name="internationalId">International expense identifier.</param>
        /// <returns>No content.</returns>
        ///  delete api/International/ClaimID/international
        [HttpDelete("{internationalId}/international")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Guid internationalId)
        {
            if (internationalId == Guid.Empty)
                return BadRequest("Invalid international expense id.");

            try
            {
                var deleted = await _internationalService.DeleteInternationalAsync(internationalId);

                if (deleted == null)
                    return NotFound($"No international expense found with id '{internationalId}'.");

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