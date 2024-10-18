using Microsoft.Extensions.Logging;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances.Coverage;
using SU.Backend.Models.Insurances;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Controllers
{
    /// <summary>
    /// This class is responsible for handling the insurance controller.
    /// Makes logic available in the Viewmodel
    /// More info about the logic for each method can be found in the Service function each controller method uses.
    /// </summary>
    public class InsuranceController
    {
        private readonly IInsuranceService _insuranceService; 
        private readonly ILogger<InsuranceController> _logger;

        public InsuranceController(IInsuranceService insuranceService, ILogger<InsuranceController> logger)
        {
            _insuranceService = insuranceService;
            _logger = logger;
        }

        public async Task<(List<Insurance> insurances, string Message)> GetAllInsurances()
        {
            _logger.LogInformation("Controller activated to list all insurances...");
            var result = await _insuranceService.GetAllInsurances();

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

        public async Task<(List<InsuranceAddonType> insuranceAddonTypes, string Message)> GetAllInsuranceAddonTypes()
        {
            _logger.LogInformation("Controller activated to list all insurance addon types...");
            var result = await _insuranceService.GetAllInsuranceAddonTypes();

            if (result.Success) 
            {
                _logger.LogInformation($"Insurance addon types retrieved succesfully:\n{result.Message}");
                return (result.InsuranceAddonTypes, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving insurance addon types: {result.Message}");
                return (new List<InsuranceAddonType>(), result.Message);
            }
        }

        public async Task<(List<InsuranceAddon> InsuranceAddons, string Message)> GetAllInsuranceAddons()
        {
            _logger.LogInformation("Controller activated to list all insurance addons...");
            var result = await _insuranceService.GetAllInsuranceAddons();

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

        public async Task<(bool Success, string Message)> CreatePrivateInsurance(PrivateCustomer privateCustomer,
            InsuranceType insuranceType,
            PrivateCoverageOption privateCoverageOption,
            Employee seller,
            bool isPolicyHolderInsured,
            string? note,
            DateTime? startDate = null,
            DateTime? endDate = null,
            List<InsuranceAddonType>? addons = null, // Nullable lista med addons
            InsuredPerson? insuredPerson = null)
        {
            _logger.LogInformation("Controller activated to create new private insurance...");
            var result = await _insuranceService.CreatePrivateInsurance(privateCustomer, insuranceType, privateCoverageOption, seller, isPolicyHolderInsured, note, startDate, endDate, addons, insuredPerson);

            if (result.Success)
            {
                _logger.LogInformation($"Private insurance created successfully:\n{result.Message}");
                return (result.Success, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error creating private insurance: {result.Message}");
                return (result.Success, result.Message);
            }
        }

        public async Task<(bool Success, string Message)> CreatePropertyInventoryInsurance(CompanyCustomer companyCustomer,
            PropertyAndInventoryCoverage propertyAndInventoryCoverage, Employee seller, string note)
        {
            _logger.LogInformation("Controller activated to create new property and inventory insurance...");
            var result = await _insuranceService.CreatePropertyInventoryInsurance(companyCustomer, propertyAndInventoryCoverage, seller, note);

            if (result.Success)
            {
                _logger.LogInformation($"Property and inventory insurance created successfully:\n{result.Message}");
                return (result.Success, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error creating property and inventory insurance: {result.Message}");
                return (result.Success, result.Message);
            }
        }

        public async Task<(bool Success, string Message)> CreateLiabilityInsurance(CompanyCustomer companyCustomer,
            LiabilityCoverage liabilityCoverage, Employee seller, string note)
        {
            _logger.LogInformation("Controller activated to create new liability insurance...");
            var result = await _insuranceService.CreateLiabilityInsurance(companyCustomer, liabilityCoverage, seller, note);

            if (result.Success)
            {
                _logger.LogInformation($"Liability insurance created successfully:\n{result.Message}");
                return (result.Success, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error creating liability insurance: {result.Message}");
                return (result.Success, result.Message);
            }
        }

        public async Task<(bool Success, string Message)> CreateVehicleInsurance(CompanyCustomer companyCustomer,
            VehicleInsuranceCoverage vehicleCoverage, Employee seller, string note)
        {
            _logger.LogInformation("Controller activated to create new vehicle insurance...");
            var result = await _insuranceService.CreateVehicleInsurance(companyCustomer, vehicleCoverage, seller, note);

            if (result.Success)
            {
                _logger.LogInformation($"Vehicle insurance created successfully:\n{result.Message}");
                return (result.Success, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error creating vehicle insurance: {result.Message}");
                return (result.Success, result.Message);
            }
        }

        public async Task<(List<VehicleInsuranceCoverage> VehicleInsuranceCoverages, string Message)> GetAllVehicleInsuranceCoverages()
        {
            _logger.LogInformation("Controller activated to list all vehicle insurance coverages...");
            var result = await _insuranceService.GetAllVehicleInsuranceCoverages();

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


        public async Task<(List<InsurancePolicyHolder> InsurancePolicyHolders, string Message)> GetAllInsurancePolicyHolders()
        {
            _logger.LogInformation("Controller activated to list all insurance policy holders...");
            var result = await _insuranceService.GetAllInsurancePolicyHolders();

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
            var result = await _insuranceService.GetAllInsuranceCoverages();

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
            var result = await _insuranceService.GetAllLiabilityCoverages();

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
            var result = await _insuranceService.GetAllLiabilityCoverageOptions();

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
            var result = await _insuranceService.GetAllRiskzones();

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
            var result = await _insuranceService.GetAllPropertyAndInventoryCoverages();

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
    }
}

