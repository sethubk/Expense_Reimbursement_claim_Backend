using System.ComponentModel.DataAnnotations;

namespace Claim_Form.Entities
{
    public class International
    {
        [Key]
        public Guid InternationalId {  get; set; }
        public DateTime Date { get; set; }
        public string SupportingNo { get; set; }
        public string Particulars { get; set; }
        public string PaymentMode { get; set; }
        public string CurrencyType { get; set; }
        public decimal Amount { get; set; }
        public decimal ConvertedAmount { get; set; }
        public string Remarks { get; set; }
        public string Screenshot { get; set; }

        public Guid TravelId { get; set; }
        public TravelDetails TravelDetails { get; set; }
    }
}
