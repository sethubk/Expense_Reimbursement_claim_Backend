using Claim_Form.Data;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
namespace Claim_Form.Repositories.Implementations
{

    public class RecentClaimRepository : IRecentClaimRepository
    {
        private readonly AppDbContext _context;
        public RecentClaimRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RecentClaim> CreateClaimAsync(RecentClaim claim)
        {
            _context.RecentClaims.Add(claim);
            await _context.SaveChangesAsync();
            return claim;
        }
        public async Task UpdateClaim(RecentClaim claim)
        {
            _context.RecentClaims.Update(claim);
            await _context.SaveChangesAsync();
        }
        public async Task<RecentClaim?> GetClaim(Guid claimId)
        {
            return await _context.RecentClaims
                .Include(c => c.TravelDetails)   // ✅ REQUIRED
                .FirstOrDefaultAsync(c => c.RecentClaimId == claimId);
        }
        public async Task<RecentClaim?> GetClaimByEmpIdAsync(Guid empId)
        {
            return await _context.RecentClaims
                .FirstOrDefaultAsync(c => c.EmpId == empId);
        }
        public async Task<List<RecentClaim>> GetClaimByEmpCode(string empCode)
        {
            return await _context.RecentClaims
                .Where(rc => rc.Employee.EmpCode == empCode)
                .ToListAsync();
        }
        public async Task DeleteDraft(string EmpCode)
        {
            var Draft = await _context.RecentClaims.Where(e => e.Status == "Draft").ToListAsync();
            _context.RecentClaims.RemoveRange(Draft);
            await _context.SaveChangesAsync();
        }

    }
}

