using Claim_Form.Dtos;

namespace Claim_Form.Services.Interface
{
    public interface IRecentClaimService
    {
        Task<RecentClaimResponseDto> CreateClaimAsync(RecentClaimDto dto, string EmpCode);
        Task<RecentClaimDto> UpdateClaimAsync(RecentClaimDto dto, string EmpCode, Guid id);
        Task<RecentClaimDto> GetClaim(Guid id);
        Task<RecentClaimResponseDto?> GetClaimByEmpID(Guid id);
        Task<RecentClaimResponseDto?> GetClaimByEmpCode(string Empcode);
        Task<bool> DeleteDraft(string EmpCode);
    }
}
