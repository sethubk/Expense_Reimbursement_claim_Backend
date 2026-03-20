using Claim_Form.Dtos;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Claim_Form.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        //[HttpPost("Claim")]
        //public async Task<IActionResult> AddExpenseAsync(Guid claimid, ExpenseDto dto)
        //{
        //    return Ok(await _expenseService.AddExpense(claimid, dto));
        //}

        [HttpPost("{claimId:guid}")]
        public async Task<IActionResult> CreateExpense([FromRoute] Guid claimId, [FromBody] List<ExpenseEntryDto> entries)
        {
            if (entries == null || entries.Count == 0)
                return BadRequest("Expense entries are required.");

            try
            {
                var result = await _expenseService.CreateBulkAsync(claimId, entries);

                if (result is null)
                {
                    return NotFound();
                }
                return Ok(result);  
            } 
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error.");
            }
        }


        /// <summary>
        /// Retrieves a specific expense by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier (GUID) of the expense to retrieve.</param>
        /// <returns>
        /// Returns the expense details if found; otherwise, an appropriate HTTP error response.
        /// </returns>
        [HttpGet("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Expense retrieved successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The provided expense id is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Expense not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "An unexpected error occurred.")]
        public async Task<IActionResult> GetExpenseAsync([FromRoute] Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("Invalid expense id.");

                var expense = await _expenseService.GetExpenseAsync(id);
                if (expense is null)
                    return NotFound($"No expense found with id '{id}'.");

                return Ok(expense);
            }
            catch (Exception ex)
            {
                // Single catch as requested
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "An unexpected error occurred.",
                    Detail = "Please try again later.",
                    Instance = HttpContext?.Request?.Path
                });
            }
        }

        /// <summary>
        /// Updates an existing expense with the provided details.
        /// </summary>
        /// <param name="id">The unique identifier (GUID) of the expense to update.</param>
        /// <param name="dto">The expense data to apply to the existing record.</param>
        /// <returns>
        /// The updated expense details if the update succeeds; otherwise, an appropriate HTTP error response.
        /// </returns>
        [HttpPut("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Expense updated successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The request is invalid or the id is not valid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Expense not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "An unexpected error occurred.")]
        public async Task<IActionResult> UpdateExpense([FromRoute] Guid id, [FromBody] ExpenseDto dto)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("Invalid expense id.");
                if (dto is null)
                    return BadRequest("Request body is required.");

                var updated = await _expenseService.UpdateExpense(id, dto);
                if (updated is null)
                    return NotFound($"No expense found with id '{id}'.");

                return Ok(updated);
            }
            catch (Exception)
            {
                // Single catch as requested
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "An unexpected error occurred.",
                    Detail = "Please try again later.",
                    Instance = HttpContext?.Request?.Path
                });
            }
        }

        /// <summary>
        /// Deletes an expense by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier (GUID) of the expense to delete.</param>
        /// <returns>
        /// Returns 204 No Content if the expense was deleted; otherwise, an appropriate HTTP error response.
        /// </returns>
        [HttpDelete("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Expense deleted successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The provided expense id is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Expense not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "An unexpected error occurred.")]
        public async Task<IActionResult> DeleteExpense([FromRoute] Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("Invalid expense id.");

                var deleted = await _expenseService.DeleteExpense(id);

               
                // If your service returns the deleted object
                if (deleted is null)
                    return NotFound($"No expense found with id '{id}'.");

                return NoContent();
            }
            catch (Exception)
            {
                // Single catch as requested
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "An unexpected error occurred.",
                    Detail = "Please try again later.",
                    Instance = HttpContext?.Request?.Path
                });
            }
        }


    }
}

