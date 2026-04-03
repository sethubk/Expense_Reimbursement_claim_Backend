using Claim_Form.Dtos;

namespace Claim_Form.Services.Interface
{
    /// <summary>
    /// Defines business operations related to international travel details.
    /// </summary>
    public interface IInternationalTravelService
    {
        /// <summary>
        /// Adds or updates travel details for a specific claim.
        /// </summary>
        /// <param name="claimId">Claim identifier.</param>
        /// <param name="dto">Travel details information.</param>
        /// <returns>Created or updated travel details.</returns>
        Task<TravelDetailsDto> AddTravelDetailsAsync(
            Guid claimId,
            TravelDetailsDto dto);

        /// <summary>
        /// Retrieves travel details associated with a specific claim.
        /// </summary>
        /// <param name="claimId">Claim identifier.</param>
        /// <returns>Travel details if found; otherwise null.</returns>
        Task<TravelDetailsDto?> GetTravelByClaimIdAsync(Guid claimId);
    }
}