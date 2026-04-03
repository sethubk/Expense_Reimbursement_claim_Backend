using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Claim_Form.Services.Interface;

namespace Claim_Form.Services.Implementations
{
    /// <summary>
    /// Service implementation for expense-related business operations.
    /// </summary>
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IRecentClaimRepository _recentClaimRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseService"/> class.
        /// </summary>
        public ExpenseService(
            IExpenseRepository expenseRepository,
            IRecentClaimRepository recentClaimRepository)
        {
            _expenseRepository = expenseRepository;
            _recentClaimRepository = recentClaimRepository;
        }

        /// <summary>
        /// Creates multiple expense entries for a claim.
        /// </summary>
        public async Task<IEnumerable<ExpenseDto>> CreateBulkAsync(
            Guid claimId,
            List<ExpenseEntryDto> entries)
        {
            var claim = await _recentClaimRepository.GetByIdAsync(claimId);

            if (claim == null)
                throw new InvalidOperationException("Claim not found.");

            var expenses = entries.Select(e => new Expense
            {
                RecentClaimId = claimId,
                Date = e.Date,
                SupportingNo = e.SupportingNo,
                Particulars = e.Particulars,
                PaymentMode = e.PaymentMode,
                Amount = e.Amount,
                Remarks = e.Remarks,
                Screenshot = e.Screenshot ?? string.Empty
            }).ToList();

            await _expenseRepository.AddBulkAsync(expenses);

            return expenses.Select(e => new ExpenseDto
            {
                Date = e.Date,
                SupportingNo = e.SupportingNo,
                Particulars = e.Particulars,
                PaymentMode = e.PaymentMode,
                Amount = e.Amount,
                Remarks = e.Remarks,
                Screenshot = e.Screenshot
            });
        }

        /// <summary>
        /// Retrieves an expense by its identifier.
        /// </summary>
        public async Task<ExpenseDto?> GetExpenseAsync(Guid id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense == null)
                return null;

            return new ExpenseDto
            {
                Date = expense.Date,
                SupportingNo = expense.SupportingNo,
                Particulars = expense.Particulars,
                PaymentMode = expense.PaymentMode,
                Amount = expense.Amount,
                Remarks = expense.Remarks,
                Screenshot = expense.Screenshot
            };
        }

        /// <summary>
        /// Updates an existing expense.
        /// </summary>
        public async Task<ExpenseDto?> UpdateExpenseAsync(Guid id, ExpenseDto dto)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense == null)
                return null;

            expense.Date = dto.Date;
            expense.SupportingNo = dto.SupportingNo;
            expense.Particulars = dto.Particulars;
            expense.PaymentMode = dto.PaymentMode;
            expense.Amount = dto.Amount;
            expense.Remarks = dto.Remarks;
            expense.Screenshot = dto.Screenshot;

            await _expenseRepository.UpdateAsync(expense);

            return dto;
        }

        /// <summary>
        /// Deletes an expense by its identifier.
        /// </summary>
        public async Task<bool> DeleteExpenseAsync(Guid id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense == null)
                return false;

            await _expenseRepository.DeleteAsync(id);
            return true;
        }
    }
}
