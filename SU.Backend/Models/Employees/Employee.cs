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
        public int employeeId { get; set; }
        public string personalNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }

        // Navigation property for EmployeeRoleAssignment
        // An employee, for eg. sales assistant takes on multiple roles. Therefor we have a collection of EmployeeRoleAssignment
        public ICollection<EmployeeRoleAssignment> roleAssignments { get; set; } = new List<EmployeeRoleAssignment>();
        public int baseSalary { get; set; }


        // Manager relation, recursive relation
        public Employee? manager { get; set; }  // Navigation property
        public int?managerId { get; set; }     // Foreign key property

        // Agent number is only relevant for sales employees
        public string? agentNumber { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        // Navigation property for sold insurances
        // This helps keep track of which employee sold which insurance for comission purposes but also prospect assignment.
        public ICollection<Insurance>? insurances { get; set; } = new List<Insurance>(); // Försäkringar sålda av anställd

    }


}



