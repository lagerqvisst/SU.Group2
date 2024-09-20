using SU.Backend.Models.Enums.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurance.Coverage
{
    public class PrivateCoverage
    {
        public int PrivateCoverageId { get; set; } // PK
        public int InsuranceCoverageId { get; set; } // FK till InsuranceCoverage
        public InsuranceCoverage InsuranceCoverage { get; set; } // Navigation till InsuranceCoverage

        public int InsuredPersonId { get; set; } // FK till InsuredPerson
        public InsuredPerson InsuredPerson { get; set; } // Navigation till InsuredPerson

        public int PrivateCoverageOptionId { get; set; } // FK till PrivateCoverageOption
        public PrivateCoverageOption PrivateCoverageOption { get; set; } // Navigation till PrivateCoverageOption
    }
}
