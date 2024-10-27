using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurances.Coverage
{
    public class VehicleInsuranceCoverage
    {
        public int VehicleInsuranceCoverageId { get; set; } // PK
        public int InsuranceCoverageId { get; set; } // FK till InsuranceCoverage
        public int RiskzoneId { get; set; } // FK till Riskzone
        public int VehicleInsuranceOptionId { get; set; } // FK till VehicleInsuranceOption

        public decimal CoverageAmount { get; set; } // Grundkostnad per månad
        public decimal MonthlyPremium { get; set; } // Månadspremie


        // Navigation properties
        public InsuranceCoverage InsuranceCoverage { get; set; } // Navigation till InsuranceCoverage
        public VehicleInsuranceOption VehicleInsuranceOption { get; set; } // Navigation till VehicleInsuranceOption
        public RiskZone RiskZone { get; set; } // Navigation till Riskzone


    }
}
