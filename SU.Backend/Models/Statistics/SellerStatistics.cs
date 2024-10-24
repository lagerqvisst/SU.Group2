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
        public string SellerName { get; set; }
        public string AgentNumber { get; set; }

        // Monthly sales data
        public List<MonthlySalesData> MonthlySales { get; set; } = new List<MonthlySalesData>();

        // Yearly totals and averages
        public int TotalYearlySales { get; set; } // Sum of all insurances sold throughout the year
        public double AverageMonthlySales { get; set; } // Average sales per month

        // Optional: Store the year for context
        public int Year { get; set; }
    }

}
