using Claim_Form.Dtos;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Claim_Form.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseController"/>.
        /// </summary>
        /// <param name="expenseService">Expense service dependency.</param>
        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        /// <summary>
        /// Creates multiple expense entries under a single claim.
        /// </summary>
        /// <param name="claimId">Claim identifier.</param>
        /// <param name="entries">List of expense entries.</param>
        /// <returns>Created expense entries.</returns>
        /// // POST api/Expense/ID/Expense
        [HttpPost("{claimId}/Expense")]
        [SwaggerResponse(StatusCodes.Status200OK, "Expenses created successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input data.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Claim not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error.")]
        public async Task<IActionResult> CreateExpense(
            [FromRoute] Guid claimId,
            [FromBody] List<ExpenseEntryDto> entries)
        {
            if (claimId == Guid.Empty)
                return BadRequest("Claim id is required.");

            if (entries == null || entries.Count == 0)
                return BadRequest("Expense entries are required.");

            try
            {
                var result = await _expenseService.CreateExpenseAsync(claimId, entries);

                if (result == null)
                    return NotFound($"No claim found with id '{claimId}'.");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while creating expense entries.");
            }
        }

        /// <summary>
        /// Retrieves an expense by its unique identifier.
        /// </summary>
        /// <param name="ExpenseId">Expense identifier.</param>
        /// <returns>Expense details.</returns>
        ///  // get api/Expense/ID/Expense
        [HttpGet("{ExpenseId}/Expense")]
        [SwaggerResponse(StatusCodes.Status200OK, "Expense retrieved successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid expense id.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Expense not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error.")]
        public async Task<IActionResult> GetExpense([FromRoute] Guid ExpenseId)
        {
            if (ExpenseId == Guid.Empty)
                return BadRequest("Invalid expense id.");

            try
            {
                var expense = await _expenseService.GetExpenseAsync(ExpenseId);

                if (expense == null)
                    return NotFound($"No expense found with id '{ExpenseId}'.");

                return Ok(expense);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while retrieving the expense.");
            }
        }

        /// <summary>
        /// Updates an existing expense.
        /// </summary>
        /// <param name="ExpenseId">Expense identifier.</param>
        /// <param name="input">Ipdated expense data.</param>
        /// <returns>Updated expense.</returns>\
        ///  // Put api/recentClaim/ID/Expense
        [HttpPut("{ExpenseId}/Expense")]
        [SwaggerResponse(StatusCodes.Status200OK, "Expense updated successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Expense not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error.")]
        public async Task<IActionResult> UpdateExpense(
            [FromRoute] Guid ExpenseId,
            [FromBody] ExpenseDto input)
        {
            if (ExpenseId == Guid.Empty)
                return BadRequest("Invalid expense id.");

            if (input == null)
                return BadRequest("Request body is required.");

            try
            {
                var updatedExpense = await _expenseService.UpdateExpenseAsync(ExpenseId, input);

                if (updatedExpense == null)
                    return NotFound($"No expense found with id '{ExpenseId}'.");

                return Ok(updatedExpense);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while updating the expense.");
            }
        }

        /// <summary>
        /// Deletes an expense by its unique identifier.
        /// </summary>
        /// <param name="ExpenseId">Expense identifier.</param>
        /// <returns>No content.</returns>
        ///  // Delete api/recentClaim/ID/Expense
        [HttpDelete("{ExpenseId}/Expense")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Expense deleted successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid expense id.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Expense not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error.")]
        public async Task<IActionResult> DeleteExpense([FromRoute] Guid ExpenseId)
        {
            if (ExpenseId == Guid.Empty)
                return BadRequest("Invalid expense id.");

            try
            {
                var deleted = await _expenseService.DeleteExpenseAsync(ExpenseId);

                if (deleted == null)
                    return NotFound($"No expense found with id '{ExpenseId}'.");

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while deleting the expense.");
            }
        }
    }
}