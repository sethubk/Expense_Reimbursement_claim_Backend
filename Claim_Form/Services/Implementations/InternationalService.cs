using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Claim_Form.Services.Interface;

namespace Claim_Form.Services.Implementations
{
    /// <summary>
    /// Service implementation for international expense operations.
    /// </summary>
    public class InternationalService : IInternationalServices
    {
        private readonly IInternationalRepository _internationalRepository;
        private readonly IRecentClaimRepository _recentClaimRepository;
        private readonly IInternationalTravelRepository _internationalTravelRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternationalService"/> class.
        /// </summary>
        public InternationalService(
            IInternationalRepository internationalRepository,
            IRecentClaimRepository recentClaimRepository,
            IInternationalTravelRepository internationalTravelRepository)
        {
            _internationalRepository = internationalRepository;
            _recentClaimRepository = recentClaimRepository;
            _internationalTravelRepository = internationalTravelRepository;
        }

        /// <summary>
        /// Creates multiple international expenses for a given claim.
        /// </summary>
        public async Task<IEnumerable<InternationalDto>> CreateBulkAsync(
            Guid claimId,
            List<InternationalDto> entries)
        {
            var claim = await _recentClaimRepository.GetByIdAsync(claimId);

            if (claim == null || claim.TravelDetails == null)
                throw new InvalidOperationException("Claim or travel details not found.");

            var travel = claim.TravelDetails;

            var expenses = entries.Select(e => new International
            {
                TravelId = travel.Id,
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

            await _internationalRepository.AddBulkAsync(expenses);

            // 🔹 Calculate reimbursement
            decimal totalExpense = expenses.Sum(e => e.ConvertedAmount);
            decimal advanceAmount = travel.AdvanceAmount;

            decimal difference = totalExpense - advanceAmount;
            string reimbursementStatus;

            if (difference > 0)
            {
                reimbursementStatus = $"Amount Payable to Employee: ₹{difference}";
            }
            else if (difference < 0)
            {
                reimbursementStatus = $"Amount Recover from Employee: ₹{Math.Abs(difference)}";
            }
            else
            {
                reimbursementStatus = "Settled";
            }

            await _internationalTravelRepository
                .UpdateReimbursementStatusAsync(travel.Id, reimbursementStatus);

            return expenses.Select(e => new InternationalDto
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
            });
        }

        /// <summary>
        /// Retrieves an international expense by its identifier.
        /// </summary>
        public async Task<InternationalDto?> GetInternationalAsync(Guid id)
        {
            var model = await _internationalRepository.GetByIdAsync(id);

            if (model == null)
                return null;

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

        /// <summary>
        /// Updates an existing international expense.
        /// </summary>
        public async Task<InternationalDto?> UpdateInternationalAsync(
            Guid id,
            InternationalDto dto)
        {
            var model = await _internationalRepository.GetByIdAsync(id);

            if (model == null)
                return null;

            model.Date = dto.Date;
            model.SupportingNo = dto.SupportingNo;
            model.Particulars = dto.Particulars;
            model.PaymentMode = dto.PaymentMode;
            model.CurrencyType = dto.CurrencyType;
            model.Amount = dto.Amount;
            model.ConvertedAmount = dto.ConvertedAmount;
            model.Remarks = dto.Remarks;
            model.Screenshot = dto.Screenshot;

            await _internationalRepository.UpdateAsync(model);

            return dto;
        }

        /// <summary>
        /// Deletes an international expense.
        /// </summary>
        public async Task<bool> DeleteInternationalAsync(Guid id)
        {
            var model = await _internationalRepository.GetByIdAsync(id);

            if (model == null)
                return false;

            await _internationalRepository.DeleteAsync(id);
            return true;
        }
    }
}