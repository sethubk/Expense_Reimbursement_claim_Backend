namespace Claim_Form.Dtos
{
    public class ExpenseDto
    {
        public DateTime Date { get; set; }
        public string SupportingNo { get; set; }
        public string Particulars { get; set; }
        public string PaymentMode { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public string Screenshot { get; set; }
    }
}
