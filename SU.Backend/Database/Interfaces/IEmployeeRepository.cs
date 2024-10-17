using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SU.Backend.Models.Employees;
using Microsoft.EntityFrameworkCore;

namespace SU.Backend.Database.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<bool> IsAgentNumberUnique(string agentNumber);

        Task<Employee?> GetEmployeeByRole(EmployeeType role);

        Task<Employee?> GetEmployeeByUserCredentials(string Username, string Password);

        Task<List<Employee>> GetSalesEmployees();

        Task<Employee> GetEmployeeById(int id);

        Task<List<EmployeeRoleAssignment>> ListAllEmployeeRoleAssignments();
    }
}
