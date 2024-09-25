using SU.Backend.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Comissions
{
    public class Commission
    {
        public Employee Seller { get; set; }

        public string SellerName { get; set; }
        public string PersonalNumber { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public double CommissionAmount { get; set; }

        public static double CalculateCommission(decimal premium)
        {
            return (double)(premium * 0.12m); // Multiply by 0.12m to keep precision and then cast to double
        }
    }



}
