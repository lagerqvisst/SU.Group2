using SU.Backend.Models.Enums.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Statistics
{
    /// <summary>
    /// Create to store the monthly sales data for each insurance type.
    /// </summary>
    public class MonthlySalesData
    {
        public int month { get; set; } // 1 = January, 2 = February, ..., 12 = December

        // Store sales count for each insurance type dynamically
        public Dictionary<InsuranceType, int> insuranceSalesCounts { get; set; } = new Dictionary<InsuranceType, int>();

        // Convenience method to get the total sales for this month
        public int totalSales => insuranceSalesCounts.Values.Sum();
    }

}
