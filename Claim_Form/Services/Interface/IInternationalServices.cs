using Claim_Form.Dtos;
using Claim_Form.Entities;

namespace Claim_Form.Services.Interface
{
    public interface IInternationalServices
    {
        Task<IEnumerable<InternationalDto>> CreateBulkAsync(Guid claimId, List<InternationalDto> entries);
        Task<InternationalDto?> GetInternationalAsync(Guid id);
        Task<InternationalDto> UpdateInternationalAsync(Guid id, InternationalDto dto);
        Task<InternationalDto?> DeleteInternationalAsync(Guid id);
    }
}
