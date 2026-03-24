using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Claim_Form.Entities
{

    public class CashInfo
    {
        public Guid id { get; set; }
        public string LoadedDate {  get; set; }
        public string Type { get; set; }
        public string  INRRate { get; set; }
        public string TotalLoaded {  get; set; }
        public Guid TravelId { get; set; }
        public TravelDetails TravelDetails { get; set; }

    }
}
