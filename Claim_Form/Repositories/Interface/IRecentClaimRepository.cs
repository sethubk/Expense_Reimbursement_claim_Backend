using Claim_Form.Entities;

namespace Claim_Form.Repositories.Interface
{
    public interface IRecentClaimRepository
    {
        Task CreateClaimAsync(RecentClaim claim);
        Task UpdateClaim(RecentClaim claim) => Task.CompletedTask;
        Task<RecentClaim> GetClaim(Guid id);
        Task<IEnumerable<RecentClaim>> GetClaims();
     
        


    }
}
