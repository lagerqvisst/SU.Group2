using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurances.Coverage
{
    public class VehicleInsuranceOption
    {
        [Key]
        public int VehicleInsuranceOptionId { get; set; } // PK
        public decimal Deductible { get; set; } // Självrisk
        public VehicleCoverageOptions OptionDescription { get; set; } // Trafik, Halv eller Helförsäkring
        public decimal OptionCost { get; set; } // Kostnad för valda typ. 

        public ICollection<VehicleInsuranceCoverage> VehicleInsuranceCoverages { get; set; } = new List<VehicleInsuranceCoverage>();

    }
}
