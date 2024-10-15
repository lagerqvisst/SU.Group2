using SU.Backend.Models.Enums.Insurance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurances.Coverage
{
    public class PrivateCoverage
    {
        public int PrivateCoverageId { get; set; } // PK
        public int InsuranceCoverageId { get; set; } // FK till InsuranceCoverage
        public int PrivateCoverageOptionId { get; set; } // FK till PrivateCoverageOption (Nullable)
        public PrivateCoverageOption PrivateCoverageOption { get; set; } // Navigation till PrivateCoverageOption
        public InsuranceCoverage InsuranceCoverage { get; set; } // Navigation till InsuranceCoverage

        // Personinformation direkt i PrivateCoverage
        public string InsuredPersonName { get; set; }
        public string InsuredPersonPersonalNumber { get; set; }
    }
}
