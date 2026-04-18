using AutoMapper;
using Claim_Form.Dtos;
using Claim_Form.Entities;

using Claim_Form.Repositories.Implementations;
using Claim_Form.Repositories.Interface;
using Claim_Form.Services.Interface;
using System.Security.Claims;

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
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternationalService"/> class.
        /// </summary>
        public InternationalService(
            IInternationalRepository internationalRepository,
            IRecentClaimRepository recentClaimRepository,
            IInternationalTravelRepository internationalTravelRepository,
            IMapper mapper)
        {
            _internationalRepository = internationalRepository;
            _recentClaimRepository = recentClaimRepository;
            _internationalTravelRepository = internationalTravelRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates multiple international expenses for a given claim.
        /// </summary>
        public async Task<IEnumerable<InternationalDto>> CreateInternationalExpense(
            Guid claimId,
            List<InternationalDto> entries)
        {
            var claim = await _recentClaimRepository.GetByIdAsync(claimId);

            if (claim == null || claim.TravelDetails == null)
                throw new InvalidOperationException("Claim or travel details not found.");
            if (claim.TravelDetails.Internationals.Any())
            {
                await _internationalRepository.DeleteExpenseAsync(claimId);
            }
            var travel = claim.TravelDetails;

            var expenses = entries.Select(e => new International
            {
                TravelId = travel.TravelId,
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
            await _internationalRepository.CreateInternationalExpenseAsync(expenses);

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

            var created= await _internationalTravelRepository
                .UpdateReimbursementStatusAsync(travel.TravelId, reimbursementStatus);

            return _mapper.Map<List<InternationalDto>>(created);
        }

        /// <summary>
        /// Retrieves an international expense by its identifier.
        /// </summary>
        public async Task<List<InternationalDto?>> GetInternationalAsync(Guid claimId)
        {
            var model = await _recentClaimRepository.GetByIdAsync(claimId);
            if (model == null)
                return null;

            return _mapper.Map<List<InternationalDto>>(model.TravelDetails.Internationals);
        }

        /// <summary>
        /// Updates an existing international expense.
        /// </summary>
        public async Task<bool> UpdateInternationalAsync(
         Guid claimId,
         List<InternationalDto> dtoList)
        {
            var claim = await _recentClaimRepository.GetByIdAsync(claimId);

            if (claim == null)
                throw new Exception("Claim not found");

            var travel = claim.TravelDetails;

            if (travel == null)
                throw new Exception("Travel not found");

            var existing = travel.Internationals
                .Where(x => !x.IsDeleted)
                .ToList();

            var incomingIds = dtoList
                .Where(x => x.Id.HasValue && x.Id != Guid.Empty)
                .Select(x => x.Id.Value)
                .ToList();

            // =========================
            // UPDATE + INSERT
            // =========================
            foreach (var dto in dtoList)
            {
                if (dto.Id.HasValue && dto.Id != Guid.Empty)
                {
                    var entity = existing.FirstOrDefault(x => x.Id == dto.Id);

                    if (entity != null)
                    {
                        entity.Date = dto.Date;
                        entity.SupportingNo = dto.SupportingNo;
                        entity.Particulars = dto.Particulars;
                        entity.PaymentMode = dto.PaymentMode;
                        entity.CurrencyType = dto.CurrencyType;
                        entity.Amount = dto.Amount;
                        entity.ConvertedAmount = dto.ConvertedAmount;
                        entity.Remarks = dto.Remarks;
                        entity.Screenshot = dto.Screenshot;

                        entity.ModifiedAt = DateTime.UtcNow;
                        entity.ModifiedBy = "SYSTEM";

                        entity.IsDeleted = false;

                        await _internationalRepository.UpdateAsync(entity);
                    }
                }
                else
                {
                    var newEntity = new International
                    {
                        Id = Guid.NewGuid(),
                        TravelId = travel.TravelId,
                        Date = dto.Date,
                        SupportingNo = dto.SupportingNo,
                        Particulars = dto.Particulars,
                        PaymentMode = dto.PaymentMode,
                        CurrencyType = dto.CurrencyType,
                        Amount = dto.Amount,
                        ConvertedAmount = dto.ConvertedAmount,
                        Remarks = dto.Remarks,
                        Screenshot = dto.Screenshot,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "SYSTEM",
                        IsDeleted = false
                    };

                    await _internationalRepository.AddAsync(newEntity);
                }
            }

            // =========================
            // SOFT DELETE
            // =========================
            var toDelete = existing
                .Where(x => !incomingIds.Contains(x.Id))
                .ToList();

            foreach (var item in toDelete)
            {
                item.IsDeleted = true;
                item.DeletedAt = DateTime.UtcNow;
                item.DeletedBy = "SYSTEM";

                await _internationalRepository.UpdateAsync(item);
            }

            // =========================
            // SINGLE SAVE CALL
            // =========================
            await _internationalRepository.SaveChangesAsync();

            var updatedTravel = await _recentClaimRepository.GetByIdAsync(claimId);

            var totalExpense = updatedTravel.TravelDetails.Internationals
                .Where(x => !x.IsDeleted)
                .Sum(x => x.ConvertedAmount);

            var advanceAmount = updatedTravel.TravelDetails.AdvanceAmount;

            decimal difference = totalExpense - advanceAmount;

            string reimbursementStatus =
                difference > 0
                    ? $"Amount Payable to Employee: ₹{difference}"
                : difference < 0
                    ? $"Amount Recover from Employee: ₹{Math.Abs(difference)}"
                : "Settled";

            // =========================
            // UPDATE TRAVEL DETAILS
            // =========================
            updatedTravel.TravelDetails.ReimbursementStatus = reimbursementStatus;

            await _internationalTravelRepository.SaveChangesAsync();

            return true;
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