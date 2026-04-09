using AutoMapper;
using Azure;
using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Claim_Form.Services.Interface;
using System.Security.Claims;

namespace Claim_Form.Services.Implementations
{
    /// <summary>
    /// Service implementation for recent-claim related operations.
    /// </summary>
    public class RecentClaimService : IRecentClaimService
    {
        private readonly IRecentClaimRepository _claimRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public RecentClaimService(
            IRecentClaimRepository claimRepository,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _claimRepository = claimRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new claim for an employee.
        /// </summary>
        public async Task<RecentClaimResponseDto> CreateClaimAsync(
            RecentClaimDto dto,
            string empCode)
        {
            var employee = await _employeeRepository.GetEmployeeByCodeAsync(empCode);

            if (employee == null)
                throw new InvalidOperationException($"Employee with code '{empCode}' not found.");

            var claim = new RecentClaim
            {
                EmpId = employee.Id,
                Type = dto.Type,
                Date = dto.Date,
                Purpose = dto.Purpose,
                Status = dto.Status,
                Amount = dto.Amount
            };

            var created = await _claimRepository.CreateAsync(claim);

            return _mapper.Map<RecentClaimResponseDto>(created);
        }

        /// <summary>
        /// Updates an existing claim.
        /// </summary>
        public async Task<RecentClaimDto?> UpdateClaimAsync(
            UpdateClaimDto dto,
            string empCode,
            Guid claimId)
        {
            var employee = await _employeeRepository.GetEmployeeByCodeAsync(empCode);
            if (employee == null)
                return null;

            var claim = await _claimRepository.GetByIdAsync(claimId);
            if (claim == null)
                return null;

            claim.Status = dto.Status;
            claim.Amount = dto.Amount;
            claim.EmpId = employee.Id;

            await _claimRepository.UpdateAsync(claim);

            //return new RecentClaimDto
            //{
            //    Type = claim.Type,
            //    Date = claim.Date,
            //    Purpose = claim.Purpose,
            //    Status = claim.Status,
            //    Amount = claim.Amount
            //};
            return _mapper.Map<RecentClaimDto>(claim);
        }

        /// <summary>
        /// Retrieves a claim by its identifier.
        /// </summary>
        public async Task<ClaimDetailResponseDto?> GetClaimAsync(Guid claimId)
        {
            var claim = await _claimRepository.GetByIdAsync(claimId);

            if (claim == null)
                return null;


            if (claim == null)
                return null;

            var response = new ClaimDetailResponseDto
            {
                RecentClaimId = claim.RecentClaimId,
                ClaimType = claim.Type
            };

            if (claim.Type == "Expense")
            {
                foreach (var e in claim.Expenses)
                {
                    response.Expenses.Add(new ExpenseDto
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

                return response;
            }

            if (claim.Type == "InternationalTravels" && claim.TravelDetails != null)
            {
                response.TravelType = claim.Type;

                response.TravelDetails = new TravelDetailsDto
                {
                    CurrencyType = claim.TravelDetails.CurrencyType,
                    TravelStartDate = claim.TravelDetails.TravelStartDate,
                    TravelEndDate = claim.TravelDetails.TravelEndDate,
                    TotalDays = claim.TravelDetails.TotalDays,
                    AdvanceAmount = claim.TravelDetails.AdvanceAmount,
                    ReimbursementStatus=claim.TravelDetails.ReimbursementStatus
                };

                foreach (var c in claim.TravelDetails.CardCashEntries)
                {
                    response.TravelDetails.CardCashEntries.Add(new CashInfoDto
                    {
                        LoadedDate = c.LoadedDate,
                        PaymentType = c.PaymentType,
                        InrRate = c.InrRate,
                        TotalLoadedAmount = c.TotalLoadedAmount
                    });
                }

                
                    foreach (var i in claim.TravelDetails.Internationals)
                    {
                        response.InternationalExpenses.Add(new InternationalDto
                        {
                            Date = i.Date,
                            SupportingNo = i.SupportingNo,
                            Particulars = i.Particulars,
                            PaymentMode = i.PaymentMode,
                            CurrencyType = i.CurrencyType,
                            Amount = i.Amount,
                            ConvertedAmount = i.ConvertedAmount,
                            Remarks = i.Remarks,
                            Screenshot = i.Screenshot
                        });
                    }
                }
            

            return response;
        }

    

        /// <summary>
        /// Retrieves all claims for an employee by employee ID.
        /// </summary>
        public async Task<List<RecentClaimResponseDto>> GetClaimByEmpIDAsync(Guid empId)
        {
            var claims = await _claimRepository.GetByEmployeeIdAsync(empId);

            return claims.Select(c => new RecentClaimResponseDto
            {
                RecentClaimId = c.RecentClaimId,
                Type = c.Type,
                Date = c.Date,
                Purpose = c.Purpose,
                Status = c.Status,
                Amount = c.Amount
            }).ToList();
        }

        /// <summary>
        /// Retrieves all claims for an employee by employee code.
        /// </summary>
        public async Task<List<RecentClaimResponseDto>> GetClaimByEmpCodeAsync(string empCode)
        {
            var employee = await _employeeRepository.GetEmployeeByCodeAsync(empCode);
            if (employee == null)
                return new List<RecentClaimResponseDto>();

            var claims = await _claimRepository.GetByEmployeeCodeAsync(empCode);

            return _mapper.Map<List<RecentClaimResponseDto>>(claims);

        }

        /// <summary>
        /// Deletes draft claims for an employee.
        /// </summary>
        public async Task<bool> DeleteDraftAsync(string empCode)
        {
            var employee = await _employeeRepository.GetEmployeeByCodeAsync(empCode);
            if (employee == null)
                return false;

            await _claimRepository.DeleteDraftAsync(empCode);
            return true;
        }
    }
}