using System.ComponentModel.DataAnnotations;

namespace Claim_Form.Dtos
{
    public class EmployeeResponseDtos
    {
        public string EmpCode { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }

        public string Email { get; set; }

        public string VenderCost { get; set; }

        public string CostCenter { get; set; }
    }
}
