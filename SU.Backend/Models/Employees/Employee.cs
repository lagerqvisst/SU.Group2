using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SU.Backend.Models.Insurances;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SU.Backend.Models.Employees
{
    public class Employee
    {

        [Key]
        public int EmployeeId { get; set; }
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // Navigation property for EmployeeRoleAssignment
        // An employee, for eg. sales assistant takes on multiple roles. Therefor we have a collection of EmployeeRoleAssignment
        public ICollection<EmployeeRoleAssignment> RoleAssignments { get; set; } = new List<EmployeeRoleAssignment>();
        public int BaseSalary { get; set; }


        // Manager relation, recursive relation
        public Employee? Manager { get; set; }  // Navigation property
        public int? ManagerId { get; set; }     // Foreign key property

        // Agent number is only relevant for sales employees
        public string? AgentNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        // Navigation property for sold insurances
        // This helps keep track of which employee sold which insurance for comission purposes but also prospect assignment.
        public ICollection<Insurance>? Insurances { get; set; } = new List<Insurance>(); // Försäkringar sålda av anställd

    }


}



