using Microsoft.EntityFrameworkCore;

namespace Claim_Form.Entities
{
    [Owned]
    public class CashInfo
    {
        
        public string LoadedDate {  get; set; }
        public string Type { get; set; }
        public string  INRRate { get; set; }
        public string TotalLoaded {  get; set; }

    }
}
