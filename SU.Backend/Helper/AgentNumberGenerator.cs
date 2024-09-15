using SU.Backend.Database.Repositories;
using System;
using System.Threading.Tasks;

namespace SU.Backend.Helper
{
    /// <summary>
    /// Class that generates a unique four digit agent number.
    /// Checks if the generated number is unique in the database.
    /// Continues to generate a new number until a unique one is found.
    /// </summary>
    public static class AgentNumberGenerator
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
    }
}
