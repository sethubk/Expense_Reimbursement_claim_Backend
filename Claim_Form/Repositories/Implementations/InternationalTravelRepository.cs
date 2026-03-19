using Claim_Form.Data;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

namespace Claim_Form.Repositories.Implementations
{
    public class InternationalTravelRepository :IInternationalTravelRepository
    {
        private readonly AppDbContext _context;
        

        public InternationalTravelRepository(AppDbContext context)

        {
            _context = context;
            
        }

        public async Task AddTravelDetails(TravelDetails travel)
        {
            _context.TravelDetails.Add(travel);
            await _context.SaveChangesAsync();
            
        }

        // ------------------------------
        // UPDATE TravelDetails (ADD MORE CashInfo ITEMS)
        // ------------------------------
        public async Task<TravelDetails> UpdateTravelDetails(TravelDetails travel)
        {
            _context.TravelDetails.Update(travel);
            await _context.SaveChangesAsync();
            return travel;
        }

        // ------------------------------
        // GET TravelDetails FOR A CLAIM
        // INCLUDING CASH ENTRIES
        // ------------------------------
        public async Task<TravelDetails?> GetTravelByClaimId(Guid claimId)
        {
            return await _context.TravelDetails
                .Include(t => t.CardCashEntries)
                .FirstOrDefaultAsync(t => t.RecentClaimId == claimId);
        }

    }
}
