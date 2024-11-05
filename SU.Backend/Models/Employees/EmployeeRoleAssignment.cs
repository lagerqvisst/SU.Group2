using SU.Backend.Models.Enums;

namespace SU.Backend.Models.Employees;

/// <summary>
///     This class represents the role assignment of an employee.
///     Created to solve the problem of having multiple roles for an employee.
/// </summary>
public class EmployeeRoleAssignment
{
    public int EmployeeRoleAssignmentId { get; set; }
    public EmployeeType Role { get; set; }
    public double Percentage { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}