using Claim_Form.Entities;

namespace Claim_Form.Repositories.Interface
{
    public interface IInternationalTravelRepository
    {
        Task<TravelDetails> AddTravelDetails(TravelDetails travel);
    }
}
