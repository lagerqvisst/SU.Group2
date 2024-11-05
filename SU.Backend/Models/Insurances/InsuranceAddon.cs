namespace SU.Backend.Models.Insurances;

/// <summary>
///     This class represents an insurance addon.
///     It has a reference to an InsuranceAddonType which represents the different types of addons.
///     Business documentation suggests that its only available for private coverages but we have made it possible for all
///     insurances...-
///     If the company decides to add addons to other insurances in the future.
/// </summary>
public class InsuranceAddon
{
    public int InsuranceAddonId { get; set; } // PK

    public int InsuranceAddonTypeId { get; set; } // FK
    public InsuranceAddonType InsuranceAddonType { get; set; } // Navigation property

    // Foreign key to PrivateInsurance
    public int InsuranceId { get; set; } // FK
    public Insurance Insurance { get; set; } // Navigation property
}