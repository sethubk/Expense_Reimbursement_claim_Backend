namespace Claim_Form.Entities
{
    public class RecentClaim
    {
        public Guid RecentClaimId { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public Guid EmpId { get; set; }
        public Employee Employee { get; set; }

        public ICollection<Expense> Expenses { get; set; }
        public ICollection<TravelDetails> TravelDetails { get; set; }
    }
}
