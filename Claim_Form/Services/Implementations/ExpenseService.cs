using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Repositories.Implementations;
using Claim_Form.Repositories.Interface;
using Claim_Form.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Claim_Form.Services.Implementations
{
    
        public class ExpenseService : IExpenseService
        {

            private readonly IRecentClaimRepository _RecentRepository;

            private readonly IExpenseRepository _ExpenseRepository;
            private readonly ILogger<ExpenseService> _logger;

            public ExpenseService(IExpenseRepository expenseRepository, IRecentClaimRepository recentClaimRepository, ILogger<ExpenseService> logger)
            {
                _ExpenseRepository = expenseRepository;
                _RecentRepository = recentClaimRepository;
                _logger = logger;
            }

            public async Task<ExpenseDto> AddExpense(Guid claimid, ExpenseEntryDto dto)
            {
                var claim1 = await _RecentRepository.GetClaim(claimid);
                if (claim1 == null)
                {
                    throw new ArgumentNullException(nameof(claim1));
                }
            byte[] screenshotBytes = null;
            if (dto.Screenshot != null)
            {
                using (var ms = new MemoryStream())
                {
                    await dto.Screenshot.CopyToAsync(ms);
                    screenshotBytes = ms.ToArray();
                }
            }

            var expense = new Expense
                {
                    RecentClaimId = claim1.RecentClaimId,
                    Date = dto.Date,
                    SupportingNo = dto.SupportingNo,
                    Particulars = dto.Particulars,
                    PaymentMode = dto.PaymentMode,
                    Amount = dto.Amount,
                    Remarks = dto.Remarks,
                    Screenshot = screenshotBytes,

                };
                await _ExpenseRepository.AddExpense(expense);

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
        public async Task<IEnumerable<ExpenseDto>> CreateBulkAsync(Guid claimId, List<ExpenseEntryDto> entries)
        {
            // (Optional) Validate the claim exists
            var claim = await _RecentRepository.GetClaim(claimId);
            if (claim == null) throw new InvalidOperationException("Claim not found.");
            
            IFormFile file = entries.FirstOrDefault()?.Screenshot;

            byte[] screenshotBytes = null;

            if (file != null && file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    screenshotBytes = ms.ToArray();
                }
            }
            var models = entries.Select(e => new Expense
            {
                RecentClaimId = claimId,
                Amount = e.Amount,
                Date = e.Date,
                Particulars = e.Particulars,
                PaymentMode = e.PaymentMode,
                Remarks = e.Remarks,
                SupportingNo = e.SupportingNo,
                Screenshot = screenshotBytes
            }).ToList();

            await _ExpenseRepository.CreateBulkAsync(models);
            return models.Select(e => new ExpenseDto
            {
                Amount = e.Amount,
                Date = e.Date,
                Particulars =e.Particulars,
                PaymentMode = e.PaymentMode,
                Remarks = e.Remarks,
                SupportingNo = e.SupportingNo,
                Screenshot = e.Screenshot



            }).ToList();
        }
        
       

            public async Task<ExpenseDto?> GetExpenseAsync(Guid id)
            {
                try
                {
                    var expense = await _ExpenseRepository.GetExpenseById(id);

                    return expense == null ? null : new ExpenseDto
                    {
                        Date = expense.Date,
                        SupportingNo = expense.SupportingNo,
                        Particulars = expense.Particulars,
                        PaymentMode = expense.PaymentMode,
                        Amount = expense.Amount,
                        Remarks = expense.Remarks,
                        Screenshot = expense.Screenshot,

                    };

                }

                catch (Exception EmployeenotFound)
                {
                    throw (EmployeenotFound);

                }
            }

            public async Task<ExpenseDto> UpdateExpense(Guid id, ExpenseDto dto)
            {
                try
                {
                    var expense = await _ExpenseRepository.GetExpenseById(id);

                    if (expense == null)
                    {
                        throw new KeyNotFoundException("Expense not found");
                    }

                    // update entity fields only once
                    expense.Date = dto.Date;
                    expense.SupportingNo = dto.SupportingNo;
                    expense.Screenshot = dto.Screenshot;
                    expense.Remarks = dto.Remarks;
                    expense.Amount = dto.Amount;
                    expense.PaymentMode = dto.PaymentMode;
                    expense.Particulars = dto.Particulars;

                    await _ExpenseRepository.UpdateExpense(expense);

                    // return updated dto
                    var result = new ExpenseDto
                    {
                        Date = expense.Date,
                        SupportingNo = expense.SupportingNo,
                        Screenshot = expense.Screenshot,
                        Remarks = expense.Remarks,
                        Amount = expense.Amount,
                        PaymentMode = expense.PaymentMode,
                        Particulars = expense.Particulars
                    };

                    return result;
                }
                catch (KeyNotFoundException ex)
                {
                    _logger.LogWarning(ex.Message);
                    throw; // send to controller
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating expense");
                    throw new ApplicationException("Unable to update expense", ex);
                }
            }

            public async Task<ExpenseDto> DeleteExpense(Guid id)
            {
                var expense = await _ExpenseRepository.GetExpenseById(id);
                if (expense == null)
                {
                    _logger.LogWarning("Expense not Found ");

                }
                await _ExpenseRepository.DeleteExpense(id);

                return new ExpenseDto
                {
                    Date = expense.Date,
                    SupportingNo = expense.SupportingNo,
                    Screenshot = expense.Screenshot,
                    Remarks = expense.Remarks,
                    Amount = expense.Amount,
                    PaymentMode = expense.PaymentMode,
                    Particulars = expense.Particulars

                };

            }
        }
    }

