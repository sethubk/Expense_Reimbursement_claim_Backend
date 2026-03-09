namespace Claim_Form.Dtos
{
    public class RecentClaimDto
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }

    }
}
