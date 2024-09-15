using Microsoft.EntityFrameworkCore;
using SU.Backend.Models;
using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbConnection context) : base(context)
        {
        }

        public async Task<bool> IsAgentNumberUnique(string agentNumber)
        {
            return !await _context.Employees.AnyAsync(e => e.AgentNumber == agentNumber);

        }

        public async Task<Employee?> GetEmployeeByRole(EmployeeType role)
        {
            return await _context.Employees
                .Where(e => e.Role == role)
                .FirstOrDefaultAsync();
        }
    }
}
