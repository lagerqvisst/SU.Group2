using SU.Backend.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Comissions
{
    /// <summary>
    /// This class is responsible for defining the properties of a commission.
    /// </summary>
    public class Commission
    {
        public Employee Seller { get; set; } // A commission is associated with 1 seller
        public string SellerName { get; set; }
        public string PersonalNumber { get; set; }

        public DateTime StartDate { get; set; } // Defines the search range for commissions
        public DateTime EndDate { get; set; }
        public double CommissionAmount { get; set; }

        //This is based on the business rules that the commission is 12% of the premium acording to provided business documentation
        //Method is used in repository class when aggregating the commissions
        public static double CalculateCommission(decimal premium)
        {
            return (double)(premium * 0.12m); // Multiply by 0.12m to keep precision and then cast to double
        }
    }



}
