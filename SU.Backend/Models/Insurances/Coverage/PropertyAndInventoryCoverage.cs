﻿using SU.Backend.Helper;

namespace SU.Backend.Models.Insurances.Coverage;

public class PropertyAndInventoryCoverage
{
    // Constructor to calculate premiums directly upon creation
    public PropertyAndInventoryCoverage(decimal propertyValue, decimal inventoryValue)
    {
        PropertyValue = propertyValue;
        InventoryValue = inventoryValue;

        PropertyPremium = PremiumCalculator.CalculatePropertyPremium(propertyValue);
        InventoryPremium = PremiumCalculator.CalculateInventoryPremium(inventoryValue);
    }

    public int PropertyAndInventoryCoverageId { get; set; } // PK
    public int InsuranceCoverageId { get; set; } // FK to InsuranceCoverage

    public InsuranceCoverage InsuranceCoverage { get; set; } // Navigation to InsuranceCoverage

    public string PropertyAddress { get; set; } // Fastighetsadress
    public decimal PropertyValue { get; set; } // Värde fastigheter
    public decimal PropertyPremium { get; set; } // Premie för fastighet
    public decimal InventoryValue { get; set; } // Värde inventarier
    public decimal InventoryPremium { get; set; } // Premie för inventarier
}