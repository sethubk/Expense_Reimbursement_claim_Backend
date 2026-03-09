namespace Claim_Form.Dtos
{
    public class EmpWithClaimDto
    {
        public string EmpCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string VenderCost { get; set; }

        public List<RecentClaimDto> RecentClaims { get; set; } = new List<RecentClaimDto>();
    }
}
