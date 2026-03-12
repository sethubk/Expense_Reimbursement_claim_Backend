using Claim_Form.Dtos;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("Claim")]
        public async Task<IActionResult> AddExpenseAsync(Guid claimid, ExpenseDto dto)
        {
            return Ok(await _expenseService.AddExpense(claimid, dto));
        }

        [HttpPost("create/{claimId:guid}")]
        public async Task<IActionResult> CreateExpense([FromRoute] Guid claimId, [FromBody] List<ExpenseEntryDto> entries)
        {
            if (entries == null || entries.Count == 0)
                return BadRequest("Expense entries are required.");

            try
            {
                var result = await _expenseService.CreateBulkAsync(claimId, entries);
                return Ok(result); // return inserted rows or a summary
            }
            catch (InvalidOperationException ex)
            {

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error.");
            }
        }
      


        [HttpGet("id:Guid")]
        public async Task<IActionResult> GetExpenseAsync(Guid id)
        {
            return Ok(await _expenseService.GetExpenseAsync(id));
        }

        [HttpPut("id:Guid")]

        public async Task<IActionResult> UpdateExpense(Guid id, ExpenseDto dto)
        {
            return Ok(await _expenseService.UpdateExpense(id, dto));
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            return Ok(await _expenseService.DeleteExpense(id));
        }

    }
}

