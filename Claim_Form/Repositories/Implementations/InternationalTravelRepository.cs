using Claim_Form.Data;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Claim_Form.Repositories.Implementations
{
    /// <summary>
    /// Repository implementation for travel details related data operations.
    /// </summary>
    public class InternationalTravelRepository : IInternationalTravelRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternationalTravelRepository"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public InternationalTravelRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds new travel details for a claim.
        /// </summary>
        /// <param name="travelDetails">Travel details entity.</param>
        public async Task AddAsync(TravelDetails travelDetails)
        {
            _context.TravelDetails.Add(travelDetails);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates existing travel details.
        /// </summary>
        /// <param name="travelDetails">Travel details entity.</param>
        /// <returns>Updated travel details.</returns>
        public async Task<TravelDetails> UpdateAsync(TravelDetails travelDetails)
        {
            _context.TravelDetails.Update(travelDetails);
            await _context.SaveChangesAsync();
            return travelDetails;
        }

        /// <summary>
        /// Updates the reimbursement status for a specific travel record.
        /// </summary>
        /// <param name="travelId">Travel identifier.</param>
        /// <param name="status">New reimbursement status.</param>
        /// <returns>Updated travel details.</returns>
        public async Task<TravelDetails?> UpdateReimbursementStatusAsync(
            Guid travelId,
            string status)
        {
            var travelDetails = await _context.TravelDetails
                .FirstOrDefaultAsync(t => t.TravelId == travelId);

            if (travelDetails == null)
                return null;

            travelDetails.ReimbursementStatus = status;

            _context.TravelDetails.Update(travelDetails);
            await _context.SaveChangesAsync();

            return travelDetails;
        }

        /// <summary>
        /// Retrieves travel details for a given claim including cash/card entries.
        /// </summary>
        /// <param name="claimId">Claim identifier.</param>
        /// <returns>Travel details if found; otherwise null.</returns>
        public async Task<TravelDetails?> GetByClaimIdAsync(Guid claimId)
        {
            return await _context.TravelDetails
                .Include(t => t.CardCashEntries)
                .Include(t => t.Internationals)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.RecentClaim.RecentClaimId == claimId);
        }

        /// <summary>
        /// Retrieves travel details by travel identifier.
        /// </summary>
        /// <param name="travelId">Travel identifier.</param>
        /// <returns>Travel details if found; otherwise null.</returns>
        public async Task<TravelDetails?> GetByIdAsync(Guid travelId)
        {
            return await _context.TravelDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TravelId == travelId);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task DeleteByTravelIdAsync(Guid travelId)
        {
            var travel = await _context.TravelDetails
                .Include(t => t.CardCashEntries) 
                .FirstOrDefaultAsync(t => t.TravelId == travelId);

            if (travel == null)
                return;

            _context.TravelDetails.Remove(travel);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCardCashEntriesAsync(Guid travelId)
        {


            var entries = await _context.CashInfos
                        .Where(c => c.TravelDetails.TravelId == travelId)
                        .ToListAsync();

            if (!entries.Any())
                return;

            _context.CashInfos.RemoveRange(entries);

           await _context.SaveChangesAsync();
        }
    }
}