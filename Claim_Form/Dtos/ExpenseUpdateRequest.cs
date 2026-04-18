namespace Claim_Form.Dtos
{
    public class ExpenseUpdateRequest
    {
        public Guid ClaimId { get; set; }
        public List<ExpenseDto> Expenses { get; set; } = new();
    }
}
