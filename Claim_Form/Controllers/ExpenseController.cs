using Claim_Form.Dtos;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("id:Guid /Expense")]
        public async Task<IActionResult> AddExpenseAsync(Guid claimid, ExpenseDto dto)
        {
            return Ok(await _expenseService.AddExpense(claimid, dto));
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

