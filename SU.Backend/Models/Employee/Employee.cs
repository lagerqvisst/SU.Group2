using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Employee
{
    public class Employee
    {

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<EmployeeRoleAssignment> RoleAssignments { get; set; } = new List<EmployeeRoleAssignment>();
        public int BaseSalary { get; set; }
        public Employee? Manager { get; set; }
        public string? AgentNumber { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

    }


}



