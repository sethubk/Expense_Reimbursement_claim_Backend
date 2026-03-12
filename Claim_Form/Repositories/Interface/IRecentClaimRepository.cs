using Claim_Form.Entities;

namespace Claim_Form.Repositories.Interface
{
    public interface IRecentClaimRepository
    {
        Task<RecentClaim> CreateClaimAsync(RecentClaim claim);
        Task UpdateClaim(RecentClaim claim) => Task.CompletedTask;
        Task<RecentClaim> GetClaim(Guid id);
        Task<RecentClaim?> GetClaimByEmpIdAsync(Guid empId);

        Task<RecentClaim?> GetClaimByEmpCode(string EmpCode);
        Task DeleteDraft(string EmpCode);
    }
}
