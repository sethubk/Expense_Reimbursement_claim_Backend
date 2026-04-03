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
        /// <param name="Id">Claim identifier.</param>
        /// <param name="entries">List of international expense entries.</param>
        /// <returns>Created international expenses.</returns>
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBulk(
            [FromRoute] Guid Id,
            [FromBody] List<InternationalDto> entries)
        {
            if (Id == Guid.Empty)
                return BadRequest("Claim id is required.");

            if (entries == null || entries.Count == 0)
                return BadRequest("International expense entries are required.");

            try
            {
                var result = await _internationalService.CreateBulkAsync(Id, entries);

                if (result == null)
                    return NotFound($"No claim found with id '{Id}'.");

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
        /// <param name="id">International expense identifier.</param>
        /// <returns>International expense details.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid international expense id.");

            try
            {
                var expense = await _internationalService.GetInternationalAsync(id);

                if (expense == null)
                    return NotFound($"No international expense found with id '{id}'.");

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
        /// <param name="id">International expense identifier.</param>
        /// <param name="dto">Updated international expense data.</param>
        /// <returns>Updated international expense.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] InternationalDto dto)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid international expense id.");

            if (dto == null)
                return BadRequest("Request body is required.");

            try
            {
                var updated = await _internationalService.UpdateInternationalAsync(id, dto);

                if (updated == null)
                    return NotFound($"No international expense found with id '{id}'.");

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
        /// <param name="id">International expense identifier.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid international expense id.");

            try
            {
                var deleted = await _internationalService.DeleteInternationalAsync(id);

                if (deleted == null)
                    return NotFound($"No international expense found with id '{id}'.");

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