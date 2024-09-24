using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurances.Coverage
{
    public class PropertyAndInventoryCoverage
    {
        public int PropertyAndInventoryCoverageId { get; set; } // PK
        public int InsuranceCoverageId { get; set; } // FK till InsuranceCoverage

        public InsuranceCoverage InsuranceCoverage { get; set; } // Navigation till InsuranceCoverage

        public string PropertyAddress { get; set; } // Fastighetsadress
        public decimal PropertyValue { get; set; } // Värde fastigheter
        public decimal PropertyPremium { get; set; } // Premie för fastighet
        public decimal InventoryValue { get; set; } // Värde inventarier
        public decimal InventoryPremium { get; set; } // Premie för inventarier
    }
}
