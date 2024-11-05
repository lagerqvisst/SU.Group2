using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances;
using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Services.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the InsuranceListingService class must implement.
/// </summary>
public interface IInsuranceListingService

{
    Task<(bool success, string message, List<Insurance> insurances)> GetAllInsurances();

    Task<(bool success, string message, List<InsuranceAddon> insuranceAddons)> GetAllInsuranceAddons();

    Task<(bool success, string message, List<InsuranceAddonType> insuranceAddonTypes)> GetAllInsuranceAddonTypes();

    Task<(bool success, string message, List<InsurancePolicyHolder> insurancePolicyHolders)>
        GetAllInsurancePolicyHolders();

    Task<(bool success, string message, List<InsuranceCoverage> insuranceCoverages)> GetAllInsuranceCoverages();

    Task<(bool success, string message, List<VehicleInsuranceCoverage> vehicleInsuranceCoverages)>
        GetAllVehicleInsuranceCoverages();

    Task<(bool success, string message, List<VehicleInsuranceOption> vehicleInsuranceOptions)>
        GetAllVehicleInsuranceOptions();

    Task<(bool success, string message, List<RiskZone> riskZones)> GetAllRiskZones();

    Task<(bool success, string message, List<LiabilityCoverage> liabilityCoverages)> GetAllLiabilityCoverages();

    Task<(bool success, string message, List<LiabilityCoverageOption> liabilityCoverageOptions)>
        GetAllLiabilityCoverageOptions();

    Task<(bool success, string message, List<PropertyAndInventoryCoverage> propertyAndInventoryCoverages)>
        GetAllPropertyAndInventoryCoverages();

    Task<(bool Success, string Message, List<PrivateCoverageOption> PrivateCoverageOptions)> GetSpecificPrivateOption(
        InsuranceType insuranceType);
}