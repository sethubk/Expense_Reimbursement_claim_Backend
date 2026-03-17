using Claim_Form.Data;
using Claim_Form.Entities;
using Claim_Form.Repositories.Interface;
using Microsoft.OpenApi.Writers;

namespace Claim_Form.Repositories.Implementations
{
    public class InternationalTravelRepository :IInternationalTravelRepository
    {
        private readonly AppDbContext _contect;
        

        public InternationalTravelRepository(AppDbContext context)

        {
            _contect = context;
            
        }

        public async Task<TravelDetails>AddTravelDetails(TravelDetails travel)
        {
            _contect.TravelDetails.Add(travel);
            await _contect.SaveChangesAsync();
            return travel;
        }
    }
}
