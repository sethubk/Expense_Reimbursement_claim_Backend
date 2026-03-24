using Claim_Form.Dtos;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Claim_Form.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternationalExpenseController : ControllerBase
    {

        private readonly IInternationalServices _service;
        private readonly ILogger<InternationalExpenseController> _logger;

        public InternationalExpenseController(
            IInternationalServices service,
            ILogger<InternationalExpenseController> logger)
        {
            _service = service;
            _logger = logger;
        }

      

        /// <summary>
        /// Bulk add international expenses to a single claim.
        /// </summary>
        [HttpPost("bulk/{claimId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<InternationalDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBulk(Guid claimId, [FromBody] List<InternationalDto> entries)
        {
            try
            {
                var result = await _service.CreateBulkAsync(claimId, entries);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bulk International Expense Failed");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get a specific international expense by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(InternationalDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var data = await _service.GetInternationalAsync(id);
                if (data == null)
                    return NotFound("International expense not found.");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving international expense");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update an existing international expense.
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(InternationalDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] InternationalDto dto)
        {
            try
            {
                var updated = await _service.UpdateInternationalAsync(id, dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating international expense");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete an international expense by ID.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(InternationalDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _service.DeleteInternationalAsync(id);
                if (deleted == null)
                    return NotFound("International expense not found.");

                return Ok(deleted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting international expense");
                return BadRequest(ex.Message);
            }
        }
    }

}

