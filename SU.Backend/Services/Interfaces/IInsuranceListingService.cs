using SU.Backend.Models.Insurances.Coverage;
using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SU.Backend.Models.Enums.Insurance;

namespace SU.Backend.Services.Interfaces
{

    /// <summary>
    /// This interface is responsible for defining the methods that the InsuranceListingService class must implement.
    /// </summary>
    public interface IInsuranceListingService

    {
        Task<(bool Success, string Message, List<Insurance> Insurances)> GetAllInsurances();

        Task<(bool Success, string Message, List<InsuranceAddon> InsuranceAddons)> GetAllInsuranceAddons();

        Task<(bool Success, string Message, List<InsuranceAddonType> InsuranceAddonTypes)> GetAllInsuranceAddonTypes();

        Task<(bool Success, string Message, List<InsurancePolicyHolder> InsurancePolicyHolders)> GetAllInsurancePolicyHolders();

        Task<(bool Success, string Message, List<InsuranceCoverage> InsuranceCoverages)> GetAllInsuranceCoverages();

        Task<(bool Success, string Message, List<VehicleInsuranceCoverage> VehicleInsuranceCoverages)> GetAllVehicleInsuranceCoverages();

        Task<(bool Success, string Message, List<VehicleInsuranceOption> VehicleInsuranceCoverages)> GetAllVehicleInsuranceOptions();

        Task<(bool Success, string Message, List<Riskzone> Riskzones)> GetAllRiskzones();

        Task<(bool Success, string Message, List<LiabilityCoverage> LiabilityCoverages)> GetAllLiabilityCoverages();

        Task<(bool Success, string Message, List<LiabilityCoverageOption> LiabilityCoverageOptions)> GetAllLiabilityCoverageOptions();

        Task<(bool Success, string Message, List<PropertyAndInventoryCoverage> PropertyAndInventoryCoverages)> GetAllPropertyAndInventoryCoverages();

        Task<(bool Success, string Message, List<PrivateCoverageOption> PrivateCoverageOptions)> GetSpecificPrivateOption(InsuranceType insuranceType);

    }
}
