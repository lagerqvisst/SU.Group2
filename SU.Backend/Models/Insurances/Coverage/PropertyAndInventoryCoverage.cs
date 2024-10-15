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

        // Konstruktor för att beräkna premier direkt vid skapandet
        public PropertyAndInventoryCoverage(decimal propertyValue, decimal inventoryValue)
        {
            PropertyValue = propertyValue;
            InventoryValue = inventoryValue;

            PropertyPremium = CalculatePropertyPremium(propertyValue);
            InventoryPremium = CalculateInventoryPremium(inventoryValue);
        }

        // Statisk metod för att beräkna fastighetspremien
        public static decimal CalculatePropertyPremium(decimal propertyValue)
        {
            const decimal premiumRate = 0.002m; // 0.2% rate
            return propertyValue * premiumRate;
        }

        // Statisk metod för att beräkna inventariepremien
        public static decimal CalculateInventoryPremium(decimal inventoryValue)
        {
            const decimal premiumRate = 0.002m; // 0.2% rate
            return inventoryValue * premiumRate;
        }

        // Statisk metod för att summera den totala premien
        public static decimal CalculateTotalPremium(decimal propertyPremium, decimal inventoryPremium)
        {
            return propertyPremium + inventoryPremium;
        }

        // Alternativ metod som returnerar totalpremien direkt
        public decimal GetTotalPremium()
        {
            return PropertyPremium + InventoryPremium;
        }
    }
}
