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
    /// <summary>
    /// This class is responsible for implementing the methods defined in the IEmployeeRepository interface.
    /// </summary>
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(Context context) : base(context)
        {
        }

        // This method is responsible for checking if the agent number is unique.
        // Random agent numbers are generated for each employee.
        // It returns true if the agent number is unique, otherwise it returns false.
        public async Task<bool> IsAgentNumberUnique(string agentNumber)
        {
            return !await _context.Employees.AnyAsync(e => e.AgentNumber == agentNumber);

        }

        // This method returns the employee with the given a role.
        public async Task<Employee?> GetEmployeeByRole(EmployeeType role)
        {
            return await _context.Employees
                .Include(e => e.RoleAssignments) 
                .Where(e => e.RoleAssignments.Any(ra => ra.Role == role && ra.Percentage > 0)) 
                .OrderByDescending(e => e.RoleAssignments
                    .Where(ra => ra.Role == role && ra.Percentage > 0)
                    .Max(ra => ra.Percentage)) 
                .FirstOrDefaultAsync(); 
        }

        // This method returns the employee with the given username and password.
        // Used for authenticating users in the login page.
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

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _context.Employees
                .Include(e => e.RoleAssignments)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<List<EmployeeRoleAssignment>> GetAllEmployeeRoleAssignments()
        {
            return await _context.EmployeeRoleAssignments.ToListAsync();
        }
    }
}
