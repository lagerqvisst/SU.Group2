using SU.Backend.Models.Insurances.Coverage;
using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{

    /// <summary>
    /// This interface is responsible for defining the methods that the InsuranceListingService class must implement.
    /// </summary>
    public interface IInsuranceListingService

    {
        Task<(bool success, string message, List<Insurance> insurances)> GetAllInsurances();

        Task<(bool success, string message, List<InsuranceAddon> insuranceAddons)> GetAllInsuranceAddons();

        Task<(bool success, string message, List<InsuranceAddonType> insuranceAddonTypes)> GetAllInsuranceAddonTypes();

        Task<(bool success, string message, List<InsurancePolicyHolder> insurancePolicyHolders)> GetAllInsurancePolicyHolders();

        Task<(bool success, string message, List<InsuranceCoverage> insuranceCoverages)> GetAllInsuranceCoverages();

        Task<(bool success, string message, List<VehicleInsuranceCoverage> vehicleInsuranceCoverages)> GetAllVehicleInsuranceCoverages();

        Task<(bool success, string message, List<VehicleInsuranceOption> vehicleInsuranceOptions)> GetAllVehicleInsuranceOptions();

        Task<(bool success, string message, List<RiskZone> riskZones)> GetAllRiskZones();

        Task<(bool success, string message, List<LiabilityCoverage> liabilityCoverages)> GetAllLiabilityCoverages();

        Task<(bool success, string message, List<LiabilityCoverageOption> liabilityCoverageOptions)> GetAllLiabilityCoverageOptions();

        Task<(bool success, string message, List<PropertyAndInventoryCoverage> propertyAndInventoryCoverages)> GetAllPropertyAndInventoryCoverages();

    }
}
