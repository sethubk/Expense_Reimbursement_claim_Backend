namespace Claim_Form.Dtos
{
    public class InternationalDto
    {
        public DateTime Date { get; set; }
        public string SupportingNo { get; set; }
        public string Particulars { get; set; }
        public string PaymentMode { get; set; }
        public string CurrencyType { get; set; }
        public decimal Amount { get; set; }
        public decimal ConvertedAmount { get; set; }
        public string Remarks { get; set; }
        public string Screenshot { get; set; }
    }
}
