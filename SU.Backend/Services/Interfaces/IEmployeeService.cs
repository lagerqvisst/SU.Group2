using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;

namespace SU.Backend.Services.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the EmployeeService class must implement.
/// </summary>
public interface IEmployeeService
{
    Task<Employee?> GetManagerForRole(EmployeeType role);

    Task<(bool success, string message, Employee employee)> GetEmployeeByRole(EmployeeType role);
    Task<(bool success, string message, List<Employee> employees)> GetAllEmployees();

    Task<(bool success, string message, Employee? employee)> GetEmployeeById(int id);

    Task<(bool success, string message, Employee employee)> CreateNewEmployee(EmployeeType role, string firstName,
        string lastName, string personalNumber);

    Task<(bool success, string message)> UpdateEmployee(Employee employee);

    Task<(bool success, string message)> DeleteEmployee(Employee employee);

    Task<(bool success, string message, List<EmployeeRoleAssignment> employeeRoleAssignments)>
        GetAllEmployeeRoleAssignments();

    Task<(bool success, string message, List<Employee> salesEmployees)> GetAllSalesEmployees();
}