namespace Claim_Form.Services.Interface
{
    public interface IMailService
    {
        Task<string> SendMailByEmpCode(string empCode, Guid ClaimID, string? imageBase64 = null);
    }
}
