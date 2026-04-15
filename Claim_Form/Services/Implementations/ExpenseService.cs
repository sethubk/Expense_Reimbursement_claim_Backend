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

            var expenses = new List<Expense>();

            foreach (var e in entries)
            {
                string imagePath = null;

                if (e.Screenshot != null)
                {
                    // Folder path
                    var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };

                    var extension = Path.GetExtension(e.Screenshot.FileName).ToLower();

                    if (!allowedExtensions.Contains(extension))
                    {
                        throw new Exception("Invalid file type");
                    }

                    var folderPath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot/uploads/expenses"
                    );

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Unique file name
                    var fileName = Guid.NewGuid().ToString() +
                                   Path.GetExtension(e.Screenshot.FileName);

                    var fullPath = Path.Combine(folderPath, fileName);

                    // Save file
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await e.Screenshot.CopyToAsync(stream);
                    }

                    // Save URL path (THIS GOES TO DB)
                    imagePath = $"/uploads/expenses/{fileName}";
                }

                expenses.Add(new Expense
                {
                    RecentClaimId = claimId,
                    Date = e.Date,
                    SupportingNo = e.SupportingNo,
                    Particulars = e.Particulars,
                    PaymentMode = e.PaymentMode,
                    Amount = e.Amount,
                    Remarks = e.Remarks,
                    Screenshot = imagePath ?? string.Empty
                });
            }

            await _expenseRepository.AddExpenseAsync(expenses);

            //return expenses.Select(e => new ExpenseDto
            //{
            //    Date = e.Date,
            //    SupportingNo = e.SupportingNo,
            //    Particulars = e.Particulars,
            //    PaymentMode = e.PaymentMode,
            //    Amount = e.Amount,
            //    Remarks = e.Remarks,
            //    Screenshot = e.Screenshot
            //});
            return _mapper.Map<List<ExpenseDto>>(expenses);
        }

        /// <summary>
        /// Retrieves an expense by its identifier.
        /// </summary>
        public async Task<ExpenseDto?> GetExpenseAsync(Guid id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense == null)
                return null;

            return _mapper.Map<ExpenseDto>(expense);
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
