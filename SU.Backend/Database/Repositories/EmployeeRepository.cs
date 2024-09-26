using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Employees;
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
        public EmployeeRepository(Context context) : base(context)
        {
        }

        public async Task<bool> IsAgentNumberUnique(string agentNumber)
        {
            return !await _context.Employees.AnyAsync(e => e.AgentNumber == agentNumber);

        }

        public async Task<Employee?> GetEmployeeByRole(EmployeeType role)
        {
            return await _context.Employees
                .Include(e => e.RoleAssignments) // Inkludera rolltilldelningar
                .Where(e => e.RoleAssignments.Any(ra => ra.Role == role && ra.Percentage > 0)) // Kontrollera att anställda har den angivna rollen
                .FirstOrDefaultAsync(); // Hämta den första matchande anställda
        }

        public async Task<Employee?> GetEmployeeByUserCredentials(string Username, string Password)
        {
            return await _context.Employees
                .Where(e => e.Username == Username && e.Password == Password)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Employee>> GetSalesEmployees()
        {
            return await _context.Employees
                .Include(e => e.RoleAssignments)
                .Where(e => e.RoleAssignments.Any(ra =>
                    (ra.Role == EmployeeType.OutsideSales || ra.Role == EmployeeType.InsideSales) &&
                    ra.Percentage > 0))
                .ToListAsync();
        }

    }
}
