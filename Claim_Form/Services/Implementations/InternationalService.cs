using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Claim_Form.Services.Interface;
using System.Security.Claims;

namespace Claim_Form.Services.Implementations
{
    public class InternationalService: IInternationalServices
    {

        private readonly IRecentClaimRepository _RecentRepository;
        private readonly IInternationalRepository _internationalRepository;
        private readonly ILogger<ExpenseService> _logger;
        public InternationalService(IInternationalRepository internationalRepository ,IRecentClaimRepository recentClaimRepository,ILogger<InternationalService> logger)
        {
            _internationalRepository = internationalRepository;
            _RecentRepository = recentClaimRepository;
            _logger = (ILogger<ExpenseService>?)logger;

        }
        
        public async Task<IEnumerable<InternationalDto>> CreateBulkAsync(Guid claimId, List<InternationalDto> entries)
        {
            // (Optional) Validate the claim exists
            var claim = await _RecentRepository.GetClaim(claimId);
            if (claim == null) throw new InvalidOperationException("Claim not found.");

            var models = entries.Select(e => new International
            {


                RecentClaimId = claimId,
                Date = e.Date,
                SupportingNo = e.SupportingNo,
                Particulars = e.Particulars,
                PaymentMode = e.PaymentMode,
                CurrencyType = e.CurrencyType,
                Amount = e.Amount,
                ConvertedAmount = e.ConvertedAmount,
                Remarks = e.Remarks,
                Screenshot = e.Screenshot


            }).ToList();

            await _internationalRepository.CreateBulkAsync(models);
            return models.Select(e => new InternationalDto
            {

                
                Date = e.Date,
                SupportingNo = e.SupportingNo,
                Particulars = e.Particulars,
                PaymentMode = e.PaymentMode,
                CurrencyType = e.CurrencyType,
                Amount = e.Amount,
                ConvertedAmount = e.ConvertedAmount,
                Remarks = e.Remarks,
                Screenshot = e.Screenshot




            }).ToList();
        }

        public async Task<InternationalDto?> GetInternationalAsync(Guid id)
        {
            try
            {
                var model = await _internationalRepository.GetInternationalExpenseById(id);
                if (model == null) return null;

                return new InternationalDto
                {
                    Date = model.Date,
                    SupportingNo = model.SupportingNo,
                    Particulars = model.Particulars,
                    PaymentMode = model.PaymentMode,
                    CurrencyType = model.CurrencyType,
                    Amount = model.Amount,
                    ConvertedAmount = model.ConvertedAmount,
                    Remarks = model.Remarks,
                    Screenshot = model.Screenshot
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving international expense", ex);
            }
        }


        public async Task<InternationalDto> UpdateInternationalAsync(Guid id, InternationalDto dto)
        {
            try
            {
                var model = await _internationalRepository.GetInternationalExpenseById(id);
                if (model == null)
                    throw new KeyNotFoundException("International expense not found");

                model.Date = dto.Date;
                model.SupportingNo = dto.SupportingNo;
                model.Particulars = dto.Particulars;
                model.PaymentMode = dto.PaymentMode;
                model.CurrencyType = dto.CurrencyType;
                model.Amount = dto.Amount;
                model.ConvertedAmount = dto.ConvertedAmount;
                model.Remarks = dto.Remarks;
                model.Screenshot = dto.Screenshot;

                await _internationalRepository.UpdateInternationalExpense(model);

                return dto;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating international expense");
                throw new ApplicationException("Unable to update international expense", ex);
            }
        }


        public async Task<InternationalDto?> DeleteInternationalAsync(Guid id)
        {
            var model = await _internationalRepository.GetInternationalExpenseById(id);
            if (model == null)
            {
                _logger.LogWarning("International expense not found");
                return null;
            }

            await _internationalRepository.DeleteInternationalExpense(id);

            return new InternationalDto
            {
                Date = model.Date,
                SupportingNo = model.SupportingNo,
                Particulars = model.Particulars,
                PaymentMode = model.PaymentMode,
                CurrencyType = model.CurrencyType,
                Amount = model.Amount,
                ConvertedAmount = model.ConvertedAmount,
                Remarks = model.Remarks,
                Screenshot = model.Screenshot
            };
        }
    }
}
