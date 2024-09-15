using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models
{
    public class Employee 
    {
        
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public EmployeeType Role { get; set; }
        public int BaseSalary { get; set; }
        public Employee? Manager { get; set; }
        public string? AgentNumber { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public Employee()
        {

        }
        public static int GetSalaryForEmployeeType(EmployeeType type)
        {
            return type switch
            {
                EmployeeType.OutsideSales => (int)Salary.Seller,
                EmployeeType.InsideSales => (int)Salary.Seller,
                EmployeeType.SalesAssistant => (int)Salary.SalesAssistant,
                EmployeeType.SalesManager => (int)Salary.SalesManager,
                EmployeeType.FinancialAssistant => (int)Salary.FinancialAssistant,
                EmployeeType.FinancialManager => (int)Salary.FinancialManager,
                EmployeeType.CEO => (int)Salary.CEO,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }


        public override string ToString()
        {
            return $"EmployeeNr: {EmployeeId}\n" +
                   $"FirstName: {FirstName}\n" +
                   $"LastName: {LastName}\n" +
                   $"Email: {Email}\n" +
                   $"Role: {Role}\n" +
                   $"Salary: {BaseSalary}\n" +
                   $"Manager: {(Manager != null ? $"{Manager.FirstName} {Manager.LastName}" : "None")}\n" +
                   $"AgentNumber: {AgentNumber}";
        }
    }


}



