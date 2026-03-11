using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Repositories.Implementations;
using Claim_Form.Repositories.Interface;
using Claim_Form.Services.Interface;
using System.Security.Claims;

namespace Claim_Form.Services.Implementations
{
    public class RecentClaimService : IRecentClaimService
    {
        private readonly IRecentClaimRepository _recentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public RecentClaimService(IRecentClaimRepository recentRepository, IEmployeeRepository employeeRepository)
        {
            _recentRepository = recentRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<RecentClaimResponseDto> CreateClaimAsync(RecentClaimDto dto, string EmpCode)
        {
            var emp = await _employeeRepository.GetEmployee(EmpCode);
            if (emp != null)
            {
                var recent = new RecentClaim
                {
                    EmpId = emp.Id,
                    Type = dto.Type,
                    Date = dto.Date,
                    Purpose = dto.Purpose,
                    Status = dto.Status,
                    Amount = dto.Amount,
                };
               var claim= await _recentRepository.CreateClaimAsync(recent);
              
                var res = new RecentClaimResponseDto
                {
                    RecentClaimId = claim.RecentClaimId,
                    Type = claim.Type,
                    Date = claim.Date,
                    Purpose = claim.Purpose,
                    Status = claim.Status,
                    Amount = claim.Amount


                };
                return res;
            }
            else
            {
                throw new InvalidOperationException($"Employee with code '{EmpCode}' not found.");
            }



            
        }
        public async Task<RecentClaimDto> UpdateClaimAsync(RecentClaimDto dto, string EmpCode, Guid id)
        {
            var emp = await _employeeRepository.GetEmployeeAsync(EmpCode);
            if (emp != null)
            {
                var claim = await _recentRepository.GetClaim(id);
                if (claim != null)
                {

                    claim.Status = dto.Status;
                    claim.Amount = dto.Amount;


                    claim.EmpId = emp.Id;








                    await _recentRepository.UpdateClaim(claim);
                }
            }
            else
            {
                return null;
            }
            var updateOutput = new RecentClaimDto
            {
                Status = dto.Status,
                Amount = dto.Amount,
            };
            return updateOutput;
        }

        // var output= await _recentRepository.GetClaim(id);

        //var output1 =new RecentClaimDto
        //{
        //     Status = output.Status,
        //     Amount = output.Amount,};
        // }

        public async Task<RecentClaimDto?> GetClaim(Guid id)
        {
            var claim = await _recentRepository.GetClaim(id);

            return claim == null ? null : new RecentClaimDto
            {
                Type = claim.Type,
                Date = claim.Date,
                Purpose = claim.Purpose,
                Status = claim.Status,
                Amount = claim.Amount,

            };

        }
        public async Task<RecentClaimResponseDto?> GetClaimByEmpID(Guid id)
        {
            var emp = await _employeeRepository.GetEmployeeById(id);
            if (emp != null)
            {
                var claim = await _recentRepository.GetClaimByEmpIdAsync(id);

                return claim == null ? null : new RecentClaimResponseDto
                {
                    RecentClaimId = claim.RecentClaimId,
                    Type = claim.Type,
                    Date = claim.Date,
                    Purpose = claim.Purpose,
                    Status = claim.Status,
                    Amount = claim.Amount,

                };
            }
            else
            {
                return null;
            }

        }
        public async Task<RecentClaimResponseDto?> GetClaimByEmpCode(string Empcode)
        {
            var emp = await _employeeRepository.GetEmployee(Empcode);
            if (emp != null)
            {
                var claim = await _recentRepository.GetClaimByEmpCode(Empcode);

                return claim == null ? null : new RecentClaimResponseDto
                {
                    RecentClaimId = claim.RecentClaimId,
                    Type = claim.Type,
                    Date = claim.Date,
                    Purpose = claim.Purpose,
                    Status = claim.Status,
                    Amount = claim.Amount,

                };
            }
            else
            {
                return null;
            }

        }
        public async Task<IEnumerable<RecentClaimDto?>> GetClaims()
        {
            var claims = await _recentRepository.GetClaims();
            return claims.Select(c => new RecentClaimDto
            {
                Type = c.Type,
                Date = c.Date,
                Amount = c.Amount,
                Status = c.Status,
            });

        }


    }
}
