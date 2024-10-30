using SU.Backend.Models.Comissions;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    /// <summary>
    /// This interface is responsible for defining the methods that the EmployeeService class must implement.
    /// </summary>
    public interface IEmployeeService
    {
        Task<(bool success, string message, Employee employee)> GenerateRandomEmployee(EmployeeType role);
        Task<Employee?> GetManagerForRole(EmployeeType role);

        Task<(bool success, string message, Employee employee)> GetEmployeeByRole(EmployeeType role);
        Task<(bool success, string message, List<Employee> employees)> GetAllEmployees();

        Task<(bool success, string message, Employee? employee)> GetEmployeeById(int id);

        Task<(bool success, string message)> CreateNewEmployee_Old(Employee employee);

        Task<(bool success, string message, Employee employee)> CreateNewEmployee(EmployeeType role, string firstName, string lastName, string personalNumber);

        Task<(bool success, string message)> UpdateEmployee(Employee employee);

        Task<(bool success, string message)> DeleteEmployee(Employee employee);

        Task<(bool success, string message, List<EmployeeRoleAssignment> employeeRoleAssignments)> GetAllEmployeeRoleAssignments();
    }
}
