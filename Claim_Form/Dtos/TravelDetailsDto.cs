namespace Claim_Form.Dtos
{
    /// <summary>
    /// Represents travel details associated with a claim.
    /// </summary>
    public class TravelDetailsDto
    {
        /// <summary>
        /// Currency type used during travel (e.g., USD, EUR).
        /// </summary>
        public string CurrencyType { get; set; } = string.Empty;

        /// <summary>
        /// Travel start date.
        /// </summary>
        public DateTime TravelStartDate { get; set; }

        /// <summary>
        /// Travel end date.
        /// </summary>
        public DateTime TravelEndDate { get; set; }

        /// <summary>
        /// Total number of travel days.
        /// </summary>
        public int TotalDays { get; set; }

        /// <summary>
        /// Advance amount taken before travel.
        /// </summary>
        public decimal AdvanceAmount { get; set; }

        /// <summary>
        /// Cash or card entries associated with the travel.
        /// </summary>
        public List<CashInfoDto> CardCashEntries { get; set; } = new();
    }
}
