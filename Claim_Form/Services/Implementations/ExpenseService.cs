using AutoMapper;
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
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseService"/> class.
        /// </summary>
        public ExpenseService(
            IExpenseRepository expenseRepository,
            IRecentClaimRepository recentClaimRepository,IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _recentClaimRepository = recentClaimRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates multiple expense entries for a claim.
        /// </summary>
        public async Task<IEnumerable<ExpenseDto>> CreateExpenseAsync(
     Guid claimId,
     List<ExpenseEntryDto> entries)
        {
            var claim = await _recentClaimRepository.GetByIdAsync(claimId);

            if (claim == null)
                throw new InvalidOperationException("Claim not found.");

            if (claim.Expenses.Any())
            {
                await _expenseRepository.DeleteAsync(claimId);
            }


           
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

                await _expenseRepository.AddExpenseAsync(expenses);

                return _mapper.Map<List<ExpenseDto>>(expenses);
            }

           
            //var existingExpenses = claim.Expenses.ToList();

            //for (int i = 0; i < existingExpenses.Count && i < entries.Count; i++)
            //{
            //    existingExpenses[i].Id=claim.Expenses.
            //    existingExpenses[i].Date = entries[i].Date;
            //    existingExpenses[i].SupportingNo = entries[i].SupportingNo;
            //    existingExpenses[i].Particulars = entries[i].Particulars;
            //    existingExpenses[i].PaymentMode = entries[i].PaymentMode;
            //    existingExpenses[i].Amount = entries[i].Amount;
            //    existingExpenses[i].Remarks = entries[i].Remarks;
            //    existingExpenses[i].Screenshot = entries[i].Screenshot ?? string.Empty;
            //}

            //await _expenseRepository.UpdateAsync(existingExpenses);

            //return _mapper.Map<List<ExpenseDto>>(existingExpenses);
        


        /// <summary>
        /// Retrieves an expense by its identifier.
        /// </summary>
        public async Task<List<ExpenseDto?>> GetExpenseAsync(Guid id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense == null)
                return null;

            return _mapper.Map<List<ExpenseDto>>(expense);
        }

        /// <summary>
        /// Updates an existing expense.
        /// </summary>
        public async Task<ExpenseDto?> UpdateExpenseAsync(Guid id, ExpenseDto dto)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense == null)
                return null;

            //expense.Date = dto.Date;
            //expense.SupportingNo = dto.SupportingNo;
            //expense.Particulars = dto.Particulars;
            //expense.PaymentMode = dto.PaymentMode;
            //expense.Amount = dto.Amount;
            //expense.Remarks = dto.Remarks;
            //expense.Screenshot = dto.Screenshot;

            //await _expenseRepository.UpdateAsync(expense);

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
