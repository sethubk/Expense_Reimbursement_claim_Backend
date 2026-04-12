using Claim_Form.Data;
using Claim_Form.Dtos;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Claim_Form.Repositories.Implementations
{
    /// <summary>
    /// Repository implementation for claim-related data operations.
    /// </summary>
    public class RecentClaimRepository : IRecentClaimRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentClaimRepository"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public RecentClaimRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new claim.
        /// </summary>
        /// <param name="claim">Claim entity.</param>
        /// <returns>Created claim.</returns>
        public async Task<RecentClaim> CreateAsync(RecentClaim claim)
        {
            _context.RecentClaims.Add(claim);
            await _context.SaveChangesAsync();
            return claim;
        }

        /// <summary>
        /// Updates an existing claim.
        /// </summary>
        /// <param name="claim">Claim entity.</param>
        public async Task UpdateAsync(RecentClaim claim)
        {
            _context.RecentClaims.Update(claim);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a claim by its identifier including travel details and expenses.
        /// </summary>
        /// <param name="claimId">Claim identifier.</param>
        /// <returns>Claim if found; otherwise null.</returns>
        public async Task<RecentClaim?> GetByIdAsync(Guid claimId)
        {

            var claim = await _context.RecentClaims
                        .AsNoTracking()
                        .FirstOrDefaultAsync(c => c.RecentClaimId == claimId);

            if (claim == null)
                return null;





            return await _context.RecentClaims
                    .AsNoTracking()
                    .Include(c => c.Expenses)
                    .Include(c => c.TravelDetails)
                    .ThenInclude(c => c.CardCashEntries)
                    .Include(c => c.TravelDetails)
                        .ThenInclude(t => t.Internationals)
                    .Include(c => c.TravelDetails)
                        .ThenInclude(t => t.Domestics)
                    .FirstOrDefaultAsync(c => c.RecentClaimId == claimId);




        }

        /// <summary>
        /// Retrieves all claims for a specific employee by employee identifier.
        /// </summary>
        /// <param name="empId">Employee identifier.</param>
        /// <returns>List of claims.</returns>
        public async Task<List<RecentClaim>> GetByEmployeeIdAsync(Guid empId)
        {
            return await _context.RecentClaims
                .Where(c => c.EmpId == empId)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves all claims for a specific employee by employee code.
        /// </summary>
        /// <param name="empCode">Employee code.</param>
        /// <returns>List of claims.</returns>
        public async Task<List<RecentClaim>> GetByEmployeeCodeAsync(string empCode)
        {
            return await _context.RecentClaims
                .Include(rc => rc.Employee)
                .Where(rc => rc.Employee.EmpCode == empCode)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Deletes draft claims for a specific employee.
        /// </summary>
        /// <param name="empCode">Employee code.</param>
        public async Task DeleteDraftAsync(string empCode)
        {
            var drafts = await _context.RecentClaims
                .Where(rc => rc.Status == "Draft" && rc.Employee.EmpCode == empCode)
                .ToListAsync();

            if (drafts.Count == 0)
                return;

            _context.RecentClaims.RemoveRange(drafts);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing claim.
        /// </summary>
        /// <param name="claim">Claim entity.</param>
        public async Task ClaimStatusAsync(RecentClaim claim)
        {
            _context.RecentClaims.Update(claim);
            await _context.SaveChangesAsync();
        }

        // admin only
        /// <summary>
        /// Admin can access the all claims
        /// </summary>
        /// <param name="claim">Claim entity.</param>
        public async Task<List<RecentClaim?>>GetAllPendingClaims()
        {
            return await _context.RecentClaims.Include(c => c.Employee).Where(c => c.Status =="pending")

                 .ToListAsync();
        }
    }
}