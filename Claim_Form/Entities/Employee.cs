using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Claim_Form.Entities
{
    [Index(nameof(EmpCode), IsUnique = true)]
    public class Employee
    {
       
        public Guid Id { get; set; }
        [Required]
        public string EmpCode { get; set; }
        [Required]
        public string passwordHash { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }

        public string Email { get; set; }

        public string VenderCost { get; set; }

        public string CostCenter { get; set; }

    }
}
