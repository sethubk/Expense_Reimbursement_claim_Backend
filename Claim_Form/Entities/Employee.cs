using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Claim_Form.Entities
{
    /// <summary>
    /// Represents an employee within the system.
    /// </summary>
    [Index(nameof(EmpCode), IsUnique = true)]
    public class Employee
    {
        /// <summary>
        /// Unique identifier of the employee.
        /// </summary>
   
        public Guid Id { get; set; }

        /// <summary>
        /// Unique employee code.
        /// </summary>
        [Required]
        public string EmpCode { get; set; } = string.Empty;

        /// <summary>
        /// Hashed employee password.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Employee full name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Employee department.
        /// </summary>
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// Employee role (e.g., Admin, Manager, User).
        /// </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// Employee email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Vendor cost reference.
        /// </summary>
        public string VendorCost { get; set; } = string.Empty;

        /// <summary>
        /// Cost center assigned to the employee.
        /// </summary>
        public string CostCenter { get; set; } = string.Empty;

        /// <summary>
        /// Claims associated with the employee.
        /// </summary>
        public ICollection<RecentClaim> RecentClaims { get; set; } = new List<RecentClaim>();
    }
}