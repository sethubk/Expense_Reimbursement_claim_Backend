using Claim_Form.Dtos;

namespace Claim_Form.Services.Interface
{
    public interface IInternationalTravelService
    {
        Task<TravelDetailsDtos> AddTravelDetails(Guid ClaimID, TravelDetailsDtos travelDetailsDtos);
        Task<TravelDetailsDtos?> GetTravelByClaimId(Guid claimId);

    }
}
