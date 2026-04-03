namespace Claim_Form.Dtos
{
    /// <summary>
    /// Represents employee information returned after a successful login
    /// or employee lookup operation.
    /// </summary>
    public class EmployeeResponseDto
    {
        /// <summary>
        /// Unique employee code.
        /// </summary>
        public string EmpCode { get; set; } = string.Empty;

        /// <summary>
        /// Employee full name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Employee department.
        /// </summary>
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// Employee role (e.g., Admin, User, Manager).
        /// </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// Employee email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Vendor or vendor cost reference.
        /// </summary>
        public string VendorCost { get; set; } = string.Empty;

        /// <summary>
        /// Cost center associated with the employee.
        /// </summary>
        public string CostCenter { get; set; } = string.Empty;
    }
}