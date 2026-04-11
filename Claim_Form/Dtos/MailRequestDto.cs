namespace Claim_Form.Dtos
{
    public class MailRequestDto
    {
        public string Empcode { get; set; }
        public Guid ClaimId { get; set; }
        public string ImageBase64 { get; set; }

    }
}
