using SU.Backend.Models.Enums;
using SU.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public interface IEmployeeRepository
    {
        Task<bool> IsAgentNumberUnique(string agentNumber);

        Task<Employee?> GetEmployeeByRole(EmployeeType role);


    }
}
