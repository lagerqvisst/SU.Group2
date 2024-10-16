using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Statistics
{
    public class InsuranceStatistics
    {
        public string InsuranceType { get; set; }
        public int TotalPolicies { get; set; }
        public decimal TotalPremium { get; set; }
        public int Month { get; set; }
    }
}
