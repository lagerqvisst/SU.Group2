using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Statistics
{
    public class SellerStatistics
    {
        // Basic information about the seller
        public string sellerName { get; set; }
        public string agentNumber { get; set; }

        // Monthly sales data
        public List<MonthlySalesData> monthlySales { get; set; } = new List<MonthlySalesData>();

        // Yearly totals and averages
        public int totalYearlySales { get; set; } // Sum of all insurances sold throughout the year
        public double averageMonthlySales { get; set; } // Average sales per month

        // Optional: Store the year for context
        public int year { get; set; }
    }

}
