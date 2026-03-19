using Claim_Form.Entities;

namespace Claim_Form.Repositories.Interface
{
    public interface IInternationalTravelRepository
    {
        Task AddTravelDetails(TravelDetails travel);

        Task<TravelDetails> UpdateTravelDetails(TravelDetails travel);
        Task<TravelDetails?> GetTravelByClaimId(Guid claimId);

    }
}
