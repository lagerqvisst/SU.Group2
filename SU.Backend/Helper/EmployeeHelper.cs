using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;

namespace SU.Backend.Helper;

/// <summary>
///     This static class contains helper methods for generating info easier for the Employee attributes
/// </summary>
public static class EmployeeHelper
{
    private static readonly Random random = new();

    // This method is used to generate a unique four digit code for an employee, used as an agent number
    public static async Task<string> GenerateUniqueFourDigitCode(IEmployeeRepository employeeRepository)
    {
        string agentNumber;
        bool isUnique;

        do
        {
            agentNumber = GenerateFourDigitCode();
            isUnique = await employeeRepository.IsAgentNumberUnique(agentNumber);
        } while (!isUnique);

        return agentNumber;
    }

    // This method is used to generate a four digit code
    public static string GenerateFourDigitCode()
    {
        var code = random.Next(1000, 10000);
        return code.ToString("D4");
    }

    // This method is used to get the salary for an employee type
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
    
    // This method is used to generate a username for an employee
    public static string GenerateEmployeeUsername(string firstName, string lastName)
    {
        //Make user name from first 2 letters of first name and last name

        var username = firstName.Substring(0, 2) + lastName.Substring(0, 2);

        return username.ToLower();
    }

    // This method is used to generate an email for an employee
    public static string GenerateEmployeeEmail(string firstName, string lastName)
    {
        return firstName.ToLower() + "." + lastName.ToLower() + "@toppinsurance.se";
    }

    // This method is used to generate a password for an employee
    public static string GenerateEmployeePassword(string firstName, string lastName)
    {
        //Take the first 2 letters of first name and last name
        var part1 = firstName.Substring(0, Math.Min(2, firstName.Length)).ToLower();
        var part2 = lastName.Substring(0, Math.Min(2, lastName.Length)).ToLower();

        // Generate a random 4-digit number
        var randomNumber = random.Next(1000, 9999).ToString();

        // Combine the parts and the number to create a password
        var password = $"{part1}{part2}{randomNumber}";

        // Add a capital letter of the last name
        password += char.ToUpper(lastName[0]);

        return password;
    }

    // This method is used to generate a unique social security number for an employee
    public static string GenerateLastFourDigits(bool isMale)
    {
        var random = new Random();
        // The first three digits are random
        var firstThreeDigits = random.Next(100, 999);

        // If the person is a man, the last digit must be odd, otherwise it must be even
        var lastDigit = isMale ? GenerateOddDigit(random) : GenerateEvenDigit(random);

        return $"{firstThreeDigits}{lastDigit}";
    }

    // This method is used to generate a odd digit
    private static int GenerateOddDigit(Random random)
    {
        int digit;
        do
        {
            digit = random.Next(0, 10);
        } while (digit % 2 == 0);

        return digit;
    }

    // This method is used to generate a even digit
    private static int GenerateEvenDigit(Random random)
    {
        int digit;
        do
        {
            digit = random.Next(0, 10);
        } while (digit % 2 != 0);

        return digit;
    }

    // This method is used to get the lowest percentage role for an employee
    public static EmployeeType GetLowestPercentageRole(List<EmployeeRoleAssignment> roleAssignments)
    {
        return roleAssignments
            .OrderBy(ra => ra.Percentage)
            .Select(ra => ra.Role)
            .FirstOrDefault();
    }

    // This method is used to update the role of an employee
    public static void UpdateEmployeeRole(Employee employee, EmployeeType newRole)
    {
        if (employee != null && (employee.RoleAssignments == null || !employee.RoleAssignments.Any()))
            return;

        employee.RoleAssignments.Clear();


        employee.RoleAssignments.Add(new EmployeeRoleAssignment
        {
            Role = newRole,
            Percentage = 100
        });
    }
}