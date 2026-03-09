using Claim_Form.Data;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using static Claim_Form.Repositories.Implementations.RecentClaimRepository;

namespace Claim_Form.Repositories.Implementations
{

    public class RecentClaimRepository : IRecentClaimRepository
    {
        private readonly AppDbContext _context;
        public RecentClaimRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateClaimAsync(RecentClaim claim)
        {
            _context.RecentClaims.Add(claim);
            await _context.SaveChangesAsync();

        }
        public async Task UpdateClaim(RecentClaim claim)
        {
            _context.RecentClaims.Update(claim);
            await _context.SaveChangesAsync();
        }

        public async Task<RecentClaim> GetClaim(Guid id)
        {
            var claim = await _context.RecentClaims.FirstOrDefaultAsync(e => e.RecentClaimId == id);
            if (claim == null)
            {
                return null;
            }
            return claim;
        }
        public async Task<IEnumerable<RecentClaim>> GetClaims(Guid id, Guid empid)
        {
            var claim = await _context.RecentClaims.Where(c => c.RecentClaimId == id && c.EmpId == empid).ToListAsync();
            if (claim == null)
            {
                return null;
            }
            return claim;
        }
        public async Task<IEnumerable<RecentClaim>> GetClaims()
        {
            return await _context.RecentClaims.ToListAsync();
        }
    }
}

