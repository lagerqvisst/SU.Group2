using SU.Backend.Models.Insurances.Coverage;
using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SU.Backend.Services.Interfaces;
using Microsoft.Extensions.Logging;
using SU.Backend.Services;
using SU.Backend.Models.Enums.Insurance;

namespace SU.Backend.Controllers
{

    /// <summary>
    /// This class is responsible for handling the insurance listing controller.
    /// Makes logic available in the Viewmodel
    /// More info about the logic for each method can be found in the Service function each controller method uses.
    /// </summary>
    public class InsuranceListingController
    {

        private readonly IInsuranceListingService _insuranceListingService;   
        private readonly ILogger<InsuranceListingController> _logger;

        public InsuranceListingController(IInsuranceListingService insuranceListingService, ILogger<InsuranceListingController> logger)
        {
            _insuranceListingService = insuranceListingService;
            _logger = logger;
        }

        public async Task<(List<VehicleInsuranceCoverage> VehicleInsuranceCoverages, string Message)> GetAllVehicleInsuranceCoverages()
        {
            _logger.LogInformation("Controller activated to list all vehicle insurance coverages...");
            var result = await _insuranceListingService.GetAllVehicleInsuranceCoverages();

            if (result.Success)
            {
                _logger.LogInformation($"Vehicle insurance coverages retrieved succesfully:\n{result.Message}");
                return (result.VehicleInsuranceCoverages, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving vehicle insurance coverages: {result.Message}");
                return (new List<VehicleInsuranceCoverage>(), result.Message);
            }
        }

        public async Task<(List<Insurance> insurances, string Message)> GetAllInsurances()
        {
            _logger.LogInformation("Controller activated to list all insurances...");
            var result = await _insuranceListingService.GetAllInsurances();

            if (result.Success)
            {
                _logger.LogInformation($"Insurances retrieved successfully:\n{result.Message}");
                return (result.Insurances, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving insurances: {result.Message}");
                return (new List<Insurance>(), result.Message);
            }
        }

        public async Task<(bool Success, List<InsuranceAddonType> insuranceAddonTypes, string Message)> GetAllInsuranceAddonTypes()
        {
            _logger.LogInformation("Controller activated to list all insurance addon types...");
            var result = await _insuranceListingService.GetAllInsuranceAddonTypes();

            if (result.Success)
            {
                _logger.LogInformation($"Insurance addon types retrieved succesfully:\n{result.Message}");
                return (true, result.InsuranceAddonTypes, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving insurance addon types: {result.Message}");
                return (true, new List<InsuranceAddonType>(), result.Message);
            }
        }

        public async Task<(List<InsuranceAddon> InsuranceAddons, string Message)> GetAllInsuranceAddons()
        {
            _logger.LogInformation("Controller activated to list all insurance addons...");
            var result = await _insuranceListingService.GetAllInsuranceAddons();

            if (result.Success)
            {
                _logger.LogInformation($"Insurance addons retrieved succesfully:\n{result.Message}");
                return (result.InsuranceAddons, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving insurance addons: {result.Message}");
                return (new List<InsuranceAddon>(), result.Message);
            }
        }

        public async Task<(List<InsurancePolicyHolder> InsurancePolicyHolders, string Message)> GetAllInsurancePolicyHolders()
        {
            _logger.LogInformation("Controller activated to list all insurance policy holders...");
            var result = await _insuranceListingService.GetAllInsurancePolicyHolders();

            if (result.Success)
            {
                _logger.LogInformation($"Insurance policy holders retrieved succesfully:\n{result.Message}");
                return (result.InsurancePolicyHolders, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving insurance policy holders: {result.Message}");
                return (new List<InsurancePolicyHolder>(), result.Message);
            }
        }

        public async Task<(List<InsuranceCoverage> InsuranceCoverages, string Message)> GetAllInsuranceCoverages()
        {
            _logger.LogInformation("Controller activated to list all insurance coverages...");
            var result = await _insuranceListingService.GetAllInsuranceCoverages();

            if (result.Success)
            {
                _logger.LogInformation($"Insurance coverages retrieved succesfully:\n{result.Message}");
                return (result.InsuranceCoverages, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving insurance coverages: {result.Message}");
                return (new List<InsuranceCoverage>(), result.Message);
            }
        }

        public async Task<(List<LiabilityCoverage> LiabilityCoverages, string Message)> GetAllLiabilityCoverages()
        {
            _logger.LogInformation("Controller activated to list all liability coverages...");
            var result = await _insuranceListingService.GetAllLiabilityCoverages();

            if (result.Success)
            {
                _logger.LogInformation($"Liability coverages retrieved succesfully:\n{result.Message}");
                return (result.LiabilityCoverages, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving liability coverages: {result.Message}");
                return (new List<LiabilityCoverage>(), result.Message);
            }
        }

        public async Task<(List<LiabilityCoverageOption> LiabilityCoverageOptions, string Message)> GetAllLiabilityCoverageOptions()
        {
            _logger.LogInformation("Controller activated to list all liability coverages...");
            var result = await _insuranceListingService.GetAllLiabilityCoverageOptions();

            if (result.Success)
            {
                _logger.LogInformation($"Liability coverages retrieved succesfully:\n{result.Message}");
                return (result.LiabilityCoverageOptions, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving liability coverages: {result.Message}");
                return (new List<LiabilityCoverageOption>(), result.Message);
            }
        }

        public async Task<(List<Riskzone> Riskzones, string Message)> GetAllRiskZones()
        {
            _logger.LogInformation("Controller activated to list all riskzones...");
            var result = await _insuranceListingService.GetAllRiskzones();

            if (result.Success)
            {
                _logger.LogInformation($"Riskzones retrieved succesfully:\n{result.Message}");
                return (result.Riskzones, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving riskzones: {result.Message}");
                return (new List<Riskzone>(), result.Message);
            }
        }

        public async Task<(List<PropertyAndInventoryCoverage> PropertyAndInventoryCoverages, string Message)> GetAllPropertyAndInventoryCoverages()
        {
            _logger.LogInformation("Controller activated to list all property and inventory coverages...");
            var result = await _insuranceListingService.GetAllPropertyAndInventoryCoverages();

            if (result.Success)
            {
                _logger.LogInformation($"Property and inventory coverages retrieved succesfully:\n{result.Message}");
                return (result.PropertyAndInventoryCoverages, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving property and inventory coverages: {result.Message}");
                return (new List<PropertyAndInventoryCoverage>(), result.Message);
            }
        }

        public async Task<(List<PrivateCoverageOption> PrivateCoverageOptions, string Message)> GetSpecificPrivateOption(InsuranceType insuranceType)
        {
            _logger.LogInformation("Controller activated to get specific private option...");
            var result = await _insuranceListingService.GetSpecificPrivateOption(insuranceType);

            if (result.Success)
            {
                _logger.LogInformation($"Private option found:\n{result.Message}");
                return (result.PrivateCoverageOptions, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error getting private option: {result.Message}");
                return (new List<PrivateCoverageOption>(), result.Message);
            }
        }
    }
}
