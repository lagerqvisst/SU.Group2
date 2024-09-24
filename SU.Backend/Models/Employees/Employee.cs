using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SU.Backend.Models.Insurances;

namespace SU.Backend.Models.Employees
{
    public class Employee
    {

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<EmployeeRoleAssignment> RoleAssignments { get; set; } = new List<EmployeeRoleAssignment>();
        public int BaseSalary { get; set; }

        // Chef/Manager relation
        public Employee? Manager { get; set; }  // Navigation property
        public int? ManagerId { get; set; }     // Foreign key property
        public string? AgentNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Navigation property for sold insurances
        public ICollection<Insurance>? Insurances { get; set; } = new List<Insurance>(); // Försäkringar sålda av anställd

    }


}



