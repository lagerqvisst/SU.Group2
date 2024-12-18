﻿namespace SU.Backend.Models.Insurances.Coverage;

/// <summary>
///     This class represents the insurance coverage that the insurance object can have.
///     This class was created to decrease the amount of null columns in the Insurance table.
/// </summary>
public class InsuranceCoverage
{
    public int InsuranceCoverageId { get; set; } // PK

    public int InsuranceId { get; set; } // FK to Insurance

    // Navigational properties to specific insurance types
    public Insurance Insurance { get; set; } // Navigation to Insurance
    public LiabilityCoverage? LiabilityCoverage { get; set; } // If it is liability insurance
    public PrivateCoverage? PrivateCoverage { get; set; } // If it is private insurance

    public PropertyAndInventoryCoverage?
        PropertyAndInventoryCoverage { get; set; } // If it is property and inventory insurance

    public VehicleInsuranceCoverage? VehicleInsuranceCoverage { get; set; } // If it is vehicle insurance
}