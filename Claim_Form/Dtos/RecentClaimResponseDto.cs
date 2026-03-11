namespace Claim_Form.Dtos
{
    public class RecentClaimResponseDto
    {
        public Guid RecentClaimId { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }


    }
}
