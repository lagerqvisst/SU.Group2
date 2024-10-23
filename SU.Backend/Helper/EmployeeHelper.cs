using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
using System;
using System.Threading.Tasks;

namespace SU.Backend.Helper
{
    /// <summary>
    /// This static class contains helper methods for generating info easier for the Employee attributes
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
                EmployeeType.CEO => (int)Salary.CEO,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static string GenerateEmployeeUsername(Name name)
        {
            //make user name from first 2 letters of first name and last name

            string username = name.First.Substring(0, 2) + name.Last.Substring(0,2);

            return username.ToLower();

        }

        public static string GenerateEmployeeEmail(string Firstname, string Lastname)
        {
            return Firstname.ToLower() + "." + Lastname.ToLower() + "@toppinsurance.se";
        }

        public static string GenerateEmployeePassword(string firstName, string lastName)
        {
            // Ta de första två bokstäverna från förnamn och efternamn
            string part1 = firstName.Substring(0, Math.Min(2, firstName.Length)).ToLower();
            string part2 = lastName.Substring(0, Math.Min(2, lastName.Length)).ToLower();

            // Generera ett slumpmässigt fyrsiffrigt nummer
            string randomNumber = Random.Next(1000, 9999).ToString();

            // Kombinera förnamnsdel, efternamnsdel och slumpmässigt nummer
            string password = $"{part1}{part2}{randomNumber}";

            // Lägg till en stor bokstav från efternamnet
            password += char.ToUpper(lastName[0]);

            return password;
        }

        public static string GenerateLastFourDigits(bool isMale)
        {
            Random random = new Random();
            // De första tre siffrorna kan vara vilka som helst
            int firstThreeDigits = random.Next(100, 999);

            // Om personen är man ska den sista siffran vara udda, annars jämn för kvinnor
            int lastDigit = isMale ? GenerateOddDigit(random) : GenerateEvenDigit(random);

            return $"{firstThreeDigits}{lastDigit}";
        }

        private static int GenerateOddDigit(Random random)
        {
            int digit;
            do
            {
                digit = random.Next(0, 10);
            } while (digit % 2 == 0);
            return digit;
        }

        private static int GenerateEvenDigit(Random random)
        {
            int digit;
            do
            {
                digit = random.Next(0, 10);
            } while (digit % 2 != 0);
            return digit;
        }

        public static EmployeeType GetLowestPercentageRole(List<EmployeeRoleAssignment> roleAssignments)
        {
            return roleAssignments
                .OrderBy(ra => ra.Percentage)
                .Select(ra => ra.Role)
                .FirstOrDefault();
        }



    }
}
