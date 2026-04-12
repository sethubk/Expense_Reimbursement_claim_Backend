using AutoMapper;
using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Repositories.Implementations;
using Claim_Form.Repositories.Interface;
using Claim_Form.Services.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Claim_Form.Services.Implementations
{
    public class DomesticService:IDomesticService
    {
        private readonly IDomesticRepository _domesticrepository;
        private readonly IRecentClaimRepository _recentClaimRepository;
        private readonly IInternationalTravelRepository _internationalTravelRepository;
        private readonly IMapper _mapper;

        public DomesticService(
            IDomesticRepository domesticRepository,
             IRecentClaimRepository recentClaimRepository,
            IInternationalTravelRepository internationalTravelRepository,
            IMapper mapper

            )
        {
            _domesticrepository = domesticRepository;
            _recentClaimRepository = recentClaimRepository;
              _internationalTravelRepository = internationalTravelRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DomesticDto>> CreateDomesticExpense(
            Guid claimId,
            List<DomesticDto> entries)
        {
            var claim = await _recentClaimRepository.GetByIdAsync(claimId);

            if (claim == null || claim.TravelDetails == null)
                throw new InvalidOperationException("Claim or travel details not found.");

            var travel = claim.TravelDetails;

            var expenses = entries.Select(e => new Domestic
            {
                TravelId = travel.TravelId,
                Date = e.Date,
                SupportingNo = e.SupportingNo,
                Particulars = e.Particulars,
                PaymentMode = e.PaymentMode,
                Amount = e.Amount,
                Remarks = e.Remarks,
                Screenshot = e.Screenshot
            }).ToList();
            await _domesticrepository.CreateDomesticExpenseAsync(expenses);

            // 🔹 Calculate reimbursement
            decimal totalExpense = expenses.Sum(e => e.Amount);
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

            var created = await _internationalTravelRepository
                .UpdateReimbursementStatusAsync(travel.TravelId, reimbursementStatus);

            return _mapper.Map<List<DomesticDto>>(created.Domestics);
            //return created.Domestics.Select(e => new DomesticDto
            //{    Date = e.Date,
            //    SupportingNo = e.SupportingNo,
            //    Particulars = e.Particulars,
            //    PaymentMode = e.PaymentMode,
            //    Amount = e.Amount,
            //    Remarks = e.Remarks,
            //    Screenshot = e.Screenshot
            //});
        }


        /// <summary>
        /// Retrieves an international expense by its identifier.
        /// </summary>
        public async Task<DomesticDto?> GetDomesticAsync(Guid id)
        {
            var model = await _domesticrepository.GetByIdAsync(id);

            if (model == null)
                return null;

            return _mapper.Map<DomesticDto>(model);
        }

        /// <summary>
        /// Updates an existing international expense.
        /// </summary>
        public async Task<DomesticDto?> UpdateDomesticAsync(
            Guid id,
            DomesticDto dto)
        {
            var model = await _domesticrepository.GetByIdAsync(id);

            if (model == null)
                return null;

            model.Date = dto.Date;
            model.SupportingNo = dto.SupportingNo;
            model.Particulars = dto.Particulars;
            model.PaymentMode = dto.PaymentMode;
            model.Amount = dto.Amount;
            model.Remarks = dto.Remarks;
            model.Screenshot = dto.Screenshot;

            await _domesticrepository.UpdateAsync(model);

            return dto;
        }

        /// <summary>
        /// Deletes an international expense.
        /// </summary>
        public async Task<bool> DeleteDomesticAsync(Guid id)
        {
            var model = await _domesticrepository.GetByIdAsync(id);

            if (model == null)
                return false;

            await _domesticrepository.DeleteAsync(id);
            return true;
        }
    }
}

