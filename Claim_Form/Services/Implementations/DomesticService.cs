using AutoMapper;
using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Migrations;
using Claim_Form.Repositories.Implementations;
using Claim_Form.Repositories.Interface;
using Claim_Form.Services.Interface;
using System.Diagnostics;
using System.Security.Claims;
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
        public async Task<List<DomesticDto?>> GetDomesticAsync(Guid id)
        {
            var model = await _recentClaimRepository.GetByIdAsync(id);

            if (model == null)
                return null;

            return _mapper.Map<List<DomesticDto>>(model.TravelDetails.Domestics);
        }

        /// <summary>
        /// Updates an existing international expense.
        /// </summary>
        public async Task<bool> UpdateDomesticAsync(
            Guid id,
            List<ExpenseDto> dtoList)
        {
            var claim = await _recentClaimRepository.GetByIdAsync(id);

            if (claim == null)
                throw new Exception("Claim not found");

            var travel = claim.TravelDetails;

            if (travel == null)
                throw new Exception("Travel not found");

            var existing = travel.Domestics
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
                     
                        entity.Amount = dto.Amount;
                       
                        entity.Remarks = dto.Remarks;
                        entity.Screenshot = dto.Screenshot;

                        entity.ModifiedAt = DateTime.UtcNow;
                        entity.ModifiedBy = "SYSTEM";

                        entity.IsDeleted = false;

                        await _domesticrepository.UpdateAsync(entity);
                    }
                }
                else
                {
                    var newEntity = new Domestic
                    {
                        Id = Guid.NewGuid(),
                        TravelId = travel.TravelId,
                        Date = dto.Date,
                        SupportingNo = dto.SupportingNo,
                        Particulars = dto.Particulars,
                        PaymentMode = dto.PaymentMode,
                        
                        Amount = dto.Amount,
                     
                        Remarks = dto.Remarks,
                        Screenshot = dto.Screenshot,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "SYSTEM",
                        IsDeleted = false
                    };

                    await _domesticrepository.AddAsync(newEntity);
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

                await _domesticrepository.UpdateAsync(item);
            }

            // =========================
            // SINGLE SAVE CALL
            // =========================
            await _domesticrepository.SaveChangesAsync();

            var updatedTravel = await _recentClaimRepository.GetByIdAsync(id);

            var totalExpense = updatedTravel.TravelDetails.Domestics
                .Where(x => !x.IsDeleted)
                .Sum(x => x.Amount);

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

