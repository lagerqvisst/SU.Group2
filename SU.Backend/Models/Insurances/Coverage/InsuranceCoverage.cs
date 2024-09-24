using SU.Backend.Models.Enums.Insurance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurances.Coverage
{
    public class InsuranceCoverage
    {
        public int InsuranceCoverageId { get; set; } // PK

        public int InsuranceId { get; set; } // FK till Insurance

        // Navigational properties till specifika försäkringsklasser
        public Insurance Insurance { get; set; } // Navigation till Insurance
        public LiabilityCoverage? LiabilityCoverage { get; set; } // Om den är ansvarsförsäkring
        public PrivateCoverage? PrivateCoverage { get; set; } // Om den är privatförsäkring
        public PropertyAndInventoryCoverage? PropertyAndInventoryCoverage { get; set; } // Om den är fastighetsförsäkring
        public VehicleInsuranceCoverage? VehicleInsuranceCoverage { get; set; } // Om den är fordonsförsäkring
    }

}
