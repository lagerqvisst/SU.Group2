using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SU.Backend.Models.Employee;

namespace SU.Backend.Database.Repositories
{
    public interface IEmployeeRepository 
    {
        Task<bool> IsAgentNumberUnique(string agentNumber);

        Task<Employee?> GetEmployeeByRole(EmployeeType role);

        Task<Employee?> GetEmployeeByUserCredentials(string Username, string Password);

    }
}
