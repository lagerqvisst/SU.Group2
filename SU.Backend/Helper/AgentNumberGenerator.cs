using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Helper
{
    public static class AgentNumberGenerator
    {
        private static readonly Random Random = new Random();

        //TODO Use future interface service to check for bool if agent number is unique, else continue to randomize number.
        public static string GenerateFourDigitCode()
        {
            // Generera ett heltal mellan 1000 och 9999
            int code = Random.Next(1000, 10000);

            // Returnera koden som en sträng
            return code.ToString("D4"); // D4 säkerställer att koden alltid är 4 siffror lång
        }

    }
}
