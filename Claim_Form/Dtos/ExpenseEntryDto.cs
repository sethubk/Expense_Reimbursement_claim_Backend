namespace Claim_Form.Dtos
{
    public class ExpenseEntryDto
    {
       
        
            public decimal Amount { get; set; }
            public DateTime Date { get; set; }           // accepts "2026-03-03" or ISO
            public string Particulars { get; set; } = "";
            public string PaymentMode { get; set; } = "";
            public string Remarks { get; set; } = "";
            public string SupportingNo { get; set; } = "";
            public string FileName { get; set; }   // <--- add this if you need it
            public IFormFile Screenshot { get; set; }      // <--- nullable; allows null
       

    }
}
