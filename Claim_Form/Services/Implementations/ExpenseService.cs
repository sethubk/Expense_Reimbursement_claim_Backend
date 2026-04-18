using AutoMapper;
using Claim_Form.Data;
using Claim_Form.Dtos;
using Claim_Form.Entities;

using Claim_Form.Repositories.Interface;
using Claim_Form.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;


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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseService"/> class.
        /// </summary>
        public ExpenseService(
            IExpenseRepository expenseRepository,
            AppDbContext context,
            IRecentClaimRepository recentClaimRepository,IMapper mapper, IHttpContextAccessor httpContextAccessor   )
        {
            _expenseRepository = expenseRepository;
            _recentClaimRepository = recentClaimRepository;
            _mapper = mapper;
            _context=context;
            _httpContextAccessor=httpContextAccessor;
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

           
          


        /// <summary>
        /// Retrieves an expense by its identifier.
        /// </summary>
        public async Task<List<ExpenseDto?>> GetExpenseAsync(Guid id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense == null)
                return null;
            var dtoList = _mapper.Map<List<ExpenseDto>>(expense);

            foreach (var dto in dtoList)
            {
                if (!string.IsNullOrEmpty(dto.Screenshot))
                {
                    // Only modify if it's NOT base64
                    if (!dto.Screenshot.StartsWith("data:image"))
                    {
                        dto.Screenshot = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/{dto.Screenshot}";
                    }
                }
            }

            return dtoList;
        }

        /// <summary>
        /// Updates an existing expense.
        /// </summary>
        //public async Task<bool> UpdateExpenseAsync(Guid claimId, List<ExpenseDto> expenses)
        //{
        //    // 🔥 Always load with tracking
        //    var claim = await _context.RecentClaims
        //        .Include(x => x.Expenses)
        //        .FirstOrDefaultAsync(x => x.RecentClaimId == claimId);

        //    if (claim == null)
        //        throw new Exception("Claim not found");

        //    var existingExpenses = claim.Expenses
        //        .Where(x => !x.IsDeleted)
        //        .ToList();

        //    // =========================
        //    // GET IDS FROM FRONTEND
        //    // =========================
        //    var incomingIds = expenses
        //        .Where(x => x.Id.HasValue && x.Id != Guid.Empty)
        //        .Select(x => x.Id.Value)
        //        .ToList();

        //    // =========================
        //    // 1️⃣ UPDATE + INSERT
        //    // =========================
        //    foreach (var dto in expenses)
        //    {
        //        if (dto.Id.HasValue && dto.Id != Guid.Empty)
        //        {
        //            // 🔥 SAFE FETCH FROM DB (avoid concurrency issue)
        //            var exp = await _context.Expenses
        //                .FirstOrDefaultAsync(x => x.Id == dto.Id);

        //            if (exp == null) continue; // skip if already deleted

        //            exp.Date = dto.Date;
        //            exp.SupportingNo = dto.SupportingNo;
        //            exp.Particulars = dto.Particulars;
        //            exp.PaymentMode = dto.PaymentMode;
        //            exp.Amount = dto.Amount;
        //            exp.Remarks = dto.Remarks;
        //            exp.Screenshot = dto.Screenshot;

        //            exp.ModifiedAt = DateTime.UtcNow;
        //            exp.ModifiedBy = "SYSTEM";

        //            _context.Expenses.Update(exp); // 🔥 IMPORTANT
        //        }
        //        else
        //        {
        //            // INSERT
        //            var newExpense = new Expense
        //            {

        //                RecentClaimId = claimId,
        //                Date = dto.Date,
        //                SupportingNo = dto.SupportingNo,
        //                Particulars = dto.Particulars,
        //                PaymentMode = dto.PaymentMode,
        //                Amount = dto.Amount,
        //                Remarks = dto.Remarks,
        //                Screenshot = dto.Screenshot,
        //                CreatedAt = DateTime.UtcNow,

        //            };

        //            _context.Expenses.Add(newExpense);
        //        }
        //    }

        //    // =========================
        //    // 2️⃣ SOFT DELETE
        //    // =========================
        //    var toDelete = existingExpenses
        //        .Where(x => !incomingIds.Contains(x.Id))
        //        .ToList();

        //    foreach (var exp in toDelete)
        //    {
        //        var exists = await _context.Expenses
        //            .AsNoTracking()
        //            .AnyAsync(x => x.Id == exp.Id);

        //        if (!exists) continue; // 🔥 avoid concurrency crash

        //        exp.IsDeleted = true;
        //        exp.DeletedAt = DateTime.UtcNow;


        //        _context.Expenses.Update(exp); // 🔥 IMPORTANT
        //    }

        //    // =========================
        //    // SAVE
        //    // =========================
        //    await _context.SaveChangesAsync();

        //    return true;
        //}
        public async Task<bool> UpdateExpenseAsync(Guid claimId, List<ExpenseDto> expenses)
        {
            // 🔥 Always load with tracking
            var claim = await _recentClaimRepository.GetByIdAsync(claimId);

            if (claim == null)
                throw new Exception("Claim not found");

            var existingExpenses = claim.Expenses
                .Where(x => !x.IsDeleted)
                .ToList();

            // =========================
            // GET IDS FROM FRONTEND
            // =========================
            var incomingIds = expenses
                .Where(x => x.Id.HasValue && x.Id != Guid.Empty)
                .Select(x => x.Id.Value)
                .ToList();

            // =========================
            // 1️⃣ UPDATE + INSERT
            // =========================
            foreach (var dto in expenses)
            {
                if (dto.Id.HasValue && dto.Id != Guid.Empty)
                {
                    var exp = await _expenseRepository.GetById(dto.Id.Value);

                    if (exp == null) continue;

                    exp.Date = dto.Date;
                    exp.SupportingNo = dto.SupportingNo;
                    exp.Particulars = dto.Particulars;
                    exp.PaymentMode = dto.PaymentMode;
                    exp.Amount = dto.Amount;
                    exp.Remarks = dto.Remarks;
                    exp.Screenshot = dto.Screenshot;

                    exp.ModifiedAt = DateTime.UtcNow;
                    exp.ModifiedBy = "SYSTEM";

                    await _expenseRepository.UpdateAsync(exp);
                }
                else
                {
                    var newExpense = new Expense
                    {
                        Id = Guid.NewGuid(), // ✅ IMPORTANT
                        RecentClaimId = claimId,
                        Date = dto.Date,
                        SupportingNo = dto.SupportingNo,
                        Particulars = dto.Particulars,
                        PaymentMode = dto.PaymentMode,
                        Amount = dto.Amount,
                        Remarks = dto.Remarks,
                        Screenshot = dto.Screenshot,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _expenseRepository.AddAsync(newExpense);
                }
            }

            // =========================
            // SOFT DELETE
            // =========================
            foreach (var exp in existingExpenses)
            {
                if (!incomingIds.Contains(exp.Id))
                {
                    var exists = await _expenseRepository.ExistsAsync(exp.Id);

                    if (!exists) continue;

                    exp.IsDeleted = true;
                    exp.DeletedAt = DateTime.UtcNow;

                    await _expenseRepository.UpdateAsync(exp);
                }
            }

            // =========================
            // SAVE
            // =========================
            await _expenseRepository.SaveChangesAsync();

            return true;
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
