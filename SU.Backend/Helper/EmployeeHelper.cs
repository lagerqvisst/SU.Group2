using SU.Backend.Database.Repositories;
using SU.Backend.Models.Enums;
using System;
using System.Threading.Tasks;

namespace SU.Backend.Helper
{
    /// <summary>
    /// Class that generates a unique four digit agent number.
    /// Checks if the generated number is unique in the database.
    /// Continues to generate a new number until a unique one is found.
    /// </summary>
    public static class EmployeeHelper
    {
        private static readonly Random Random = new Random();

        public static async Task<string> GenerateUniqueFourDigitCode(IEmployeeRepository employeeRepository)
        {
            string agentNumber;
            bool isUnique;

            do
            {
                agentNumber = GenerateFourDigitCode();
                isUnique = await employeeRepository.IsAgentNumberUnique(agentNumber);
            }
            while (!isUnique);

            return agentNumber;
        }

        public static string GenerateFourDigitCode()
        {
            int code = Random.Next(1000, 10000);
            return code.ToString("D4");
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
    }
}
