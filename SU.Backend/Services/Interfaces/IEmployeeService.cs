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
    public interface IEmployeeService
    {
        Task<(bool Success, string Message, Employee Employee)> GenerateRandomEmployee(EmployeeType Role);
        Task<Employee?> GetManagerForRole(EmployeeType role);

        Task<(bool Success, string Message, List<Employee?> Employees)> ListAllEmployees();

        Task<(bool Success, string Message, Employee? Employee)> GetEmployeeById(int id);

        Task<(bool Success, string Message)> CreateNewEmployee(Employee employee);

        Task<(bool Success, string Message)> UpdateEmployee(Employee employee);

        Task<(bool Success, string Message)> DeleteEmployee(Employee employee);
    }
}
