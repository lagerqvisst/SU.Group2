using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Helper
{
    public class PrivateCustomerHelper
    {
        
        public static string GenerateCompletePersonalNumber(Result result)
        {
            // Steg 1: Formatera personens födelsedatum (YYYYMMDD)
            string formattedDateOfBirth = FormatPersonalNumber(result.Dob);

            // Steg 2: Generera de sista fyra siffrorna (tre siffror + kontrollsiffra)
            string lastFourDigits = SetLastFourDigitsOfPersonalNumber(result);

            // Steg 3: Bygg det kompletta personnumret genom att kombinera födelsedatum och de sista fyra siffrorna
            return formattedDateOfBirth + "-" + lastFourDigits;
        }
        public static string FormatPersonalNumber(DateOfBirth dob)
        {
            // Formatera födelsedatumet till formatet "YYYYMMDD"
            return dob.Date.ToString("yyyyMMdd");
        }
        public static string SetLastFourDigitsOfPersonalNumber(Result result)
        {
            // Steg 1: Generera ett tresiffrigt tal baserat på kön (ojämnt för män, jämnt för kvinnor)
            Random random = new Random();
            int lastThreeDigits;

            if (result.Gender.ToLower() == "male")
            {
                // Generera ett ojämnt tal mellan 001 och 999
                do
                {
                    lastThreeDigits = random.Next(1, 1000);
                } while (lastThreeDigits % 2 == 0); // Säkerställ att det är ojämnt
            }
            else if (result.Gender.ToLower() == "female")
            {
                // Generera ett jämnt tal mellan 000 och 998
                do
                {
                    lastThreeDigits = random.Next(0, 1000);
                } while (lastThreeDigits % 2 != 0); // Säkerställ att det är jämnt
            }
            else
            {
                throw new ArgumentException("Unknown gender");
            }

            // Steg 2: Konvertera till en tresiffrig sträng med nollutfyllning
            string lastThree = lastThreeDigits.ToString("D3");

            // Steg 3: Hämta födelsedatumets åtta siffror (YYYYMMDD)
            string personalNumberWithoutLastFour = FormatPersonalNumber(result.Dob);

            // Steg 4: Beräkna kontrollsiffran med Luhn-algoritmen
            string partialPersonalNumber = personalNumberWithoutLastFour + lastThree;
            int controlDigit = CalculateLuhnDigit(partialPersonalNumber);

            // Steg 5: Returnera de sista fyra siffrorna (tre siffror + kontrollsiffra)
            return lastThree + controlDigit.ToString();
        }

        // Luhn-algoritmen för att beräkna sista kontrollsiffran
        public static int CalculateLuhnDigit(string number)
        {
            int sum = 0;
            bool alternate = false;

            for (int i = number.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(number[i].ToString());

                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                    {
                        n -= 9;
                    }
                }

                sum += n;
                alternate = !alternate;
            }

            return (10 - (sum % 10)) % 10;
        }

    }
}
