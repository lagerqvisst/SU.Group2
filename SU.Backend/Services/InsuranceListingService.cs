using SU.Backend.Database;
using SU.Backend.Models.Insurances.Coverage;
using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SU.Backend.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace SU.Backend.Services
{
    /// <summary>
    /// This class contains methods for listing different types of insurances.
    /// </summary>
    public class InsuranceListingService : IInsuranceListingService
    {
            private readonly UnitOfWork _unitOfWork;
            private readonly ILogger<InsuranceCreateService> _logger;

            public InsuranceListingService(UnitOfWork unitOfWork, ILogger<InsuranceCreateService> logger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
            }
            
            //This method fetches all insurance policy holders from the database. The list of data will be used in the view for listing all insurance policy holders.
            public async Task<(bool Success, string Message, List<InsurancePolicyHolder> InsurancePolicyHolders)> GetAllInsurancePolicyHolders()
            {
                _logger.LogInformation("Controller activated to list all insurance policy holders...");

                try
                {
                    var insurancePolicyHolders = _unitOfWork.InsurancePolicyHolders.GetAllInsurancePolicyHolders();
                    _logger.LogInformation("Insurance policy holders retrieved succesfully: {InsurancePolicyHolderCount}", insurancePolicyHolders.Result.Count);

                    return (true, "Insurance policy holders retrieved successfully.", insurancePolicyHolders.Result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while fetching insurance policy holders.");
                    return (false, "An error occurred while fetching the insurance policy holders.", new List<InsurancePolicyHolder>());
                }
            }

        // This method fetches all insurance addon types from the database. The list of data will be used in the view for listing all insurance addon types.
        public async Task<(bool Success, string Message, List<InsuranceAddonType> InsuranceAddonTypes)> GetAllInsuranceAddonTypes()
        {
            _logger.LogInformation("Controller activated to get all insurance addon types...");

            try
            {
                var insuranceAddonTypes = _unitOfWork.InsuranceAddonTypes.GetAllInsuranceAddonTypes();
                _logger.LogInformation("Insurance addon types found: {InsuranceAddonTypeCount}", insuranceAddonTypes.Result.Count);

                return (true, "Insurance addon types found", insuranceAddonTypes.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching insurance addont types.");
                return (false, "An error occurred while fetching the insurance addon types.", new List<InsuranceAddonType>());
            }
        }
            // This method fetches all insurance addons from the database. The list of data will be used in the view for listing all insurance addons.
            public async Task<(bool Success, string Message, List<InsuranceAddon> InsuranceAddons)> GetAllInsuranceAddons()
            {
                _logger.LogInformation("Controller activated to get all insurance addons...");

                try
                {
                    var insuranceaddons = _unitOfWork.InsuranceAddons.GetAllInsuranceAddons();
                    _logger.LogInformation("Insurance addons found: {InsuranceAddonCount}", insuranceaddons.Result.Count);

                    return (true, "Insurance addons found", insuranceaddons.Result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while fetching insurance addons.");
                    return (false, "An error occurred while fetching the insurance addons.", new List<InsuranceAddon>());
                }
            }
            
            // This method fetches all insurances from the database. The list of data will be used in the view for listing all insurances.
            public async Task<(bool Success, string Message, List<Insurance> Insurances)> GetAllInsurances()
            {
                _logger.LogInformation("Controller activated to get all insurances...");

                try
                {
                    var insurances = _unitOfWork.Insurances.GetAllInsurances();
                    _logger.LogInformation("Insurances found: {InsuranceCount}", insurances.Result.Count);

                    return (true, "Insurances found", insurances.Result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while fetching insurances.");
                    return (false, "An error occurred while fetching the insurances.", new List<Insurance>());
                }
            }
            
            // This method fetches all insurance coverages from the database. The list of data will be used in the view for listing all insurance coverages.
            public async Task<(bool Success, string Message, List<InsuranceCoverage> InsuranceCoverages)> GetAllInsuranceCoverages()
            {
                _logger.LogInformation("Controller activated to get all insurance coverages...");

                try
                {
                    var insuranceCoverage = _unitOfWork.InsuranceCoverages.GetAllInsuranceCoverages();
                    _logger.LogInformation("Insurance coverages found: {InsuranceCoverageCount}", insuranceCoverage.Result.Count);

                    return (true, "Insurance coverages found", insuranceCoverage.Result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while fetching insurance coverages.");
                    return (false, "An error occurred while fetching the insurance coverages.", new List<InsuranceCoverage>());
                }
            }

            // This method fetches all vehicle insurance coverages from the database. The list of data will be used in the view for listing all vehicle insurance coverages.
            public async Task<(bool Success, string Message, List<VehicleInsuranceCoverage> VehicleInsuranceCoverages)> GetAllVehicleInsuranceCoverages()
            {
                _logger.LogInformation("Controller activated to list all vehicle insurance coverages...");

                try
                {
                    var vehicleInsuranceCoverages = _unitOfWork.VehicleInsuranceCoverages.GetAllVehicleInsuranceCoverages();
                    _logger.LogInformation("Vehicle insurance coverages retrieved succesfully: {VehicleInsuranceCoveragesCount}",
                        vehicleInsuranceCoverages.Result.Count);

                    return (true, "Vehicle insurance coverages retrieved successfully.", vehicleInsuranceCoverages.Result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while fetching vehicle insurance coverages.");
                    return (false, "An error occurred while fetching the vehicle insurance coverages.", new List<VehicleInsuranceCoverage>());
                }
            }
            
            // This method fetches all vehicle insurance options from the database. The list of data will be used in the view for listing all vehicle insurance options.
            public async Task<(bool Success, string Message, List<VehicleInsuranceOption> VehicleInsuranceCoverages)> GetAllVehicleInsuranceOptions()
            {
                _logger.LogInformation("Controller activated to list all vehicle insurance coverage options...");

                try
                {
                    var vehicleInsuranceOptions = _unitOfWork.VehicleInsuranceOptions.GetVehicleInsuranceOptions();
                    _logger.LogInformation("Vehicle insurance options retrieved succesfully: {VehicleInsuranceOptionsCount}",
                                           vehicleInsuranceOptions.Result.Count);

                    return (true, "Vehicle insurance options retrieved successfully.", vehicleInsuranceOptions.Result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while fetching vehicle insurance options.");
                    return (false, "An error occurred while fetching the vehicle insurance options.", new List<VehicleInsuranceOption>());
                }
            }

            // This method fetches all riskzones from the database. The list of data will be used in the view for listing and choosing a riskzone.
            public async Task<(bool Success, string Message, List<Riskzone> Riskzones)> GetAllRiskzones()
            {
                _logger.LogInformation("Controller activated to list all riskzones...");

                try
                {
                    var riskzones = _unitOfWork.Riskzones.GetAllRiskZones();
                    _logger.LogInformation("Riskzones retrieved succesfully: {RiskzonesCount}",
                                                              riskzones.Result.Count);

                    return (true, "Riskzones retrieved successfully.", riskzones.Result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while fetching riskzones.");
                    return (false, "An error occurred while fetching the riskzones.", new List<Riskzone>());
                }
            }

            // This method fetches all liability coverage options from the database. The list of data will be used in the view for listing all liability coverage options.
            public async Task<(bool Success, string Message, List<LiabilityCoverageOption> LiabilityCoverageOptions)> GetAllLiabilityCoverageOptions()
            {
                _logger.LogInformation("Controller activated to list all liability coverage options...");

                try
                {
                    var liabilityCoverageOptions = _unitOfWork.LiabilityCoverageOptions.GetLiabilityCoverageOptions();
                    _logger.LogInformation("Liability coverage options retrieved succesfully: {LiabilityCoverageOptionsCount}",
                                                              liabilityCoverageOptions.Result.Count);

                    return (true, "Liability coverage options retrieved successfully.", liabilityCoverageOptions.Result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while fetching liability coverage options.");
                    return (false, "An error occurred while fetching the liability coverage options.", new List<LiabilityCoverageOption>());
                }
            }
            
            // This method fetches all liability coverages from the database. The list of data will be used in the view for listing all liability coverages.
            public async Task<(bool Success, string Message, List<LiabilityCoverage> LiabilityCoverages)> GetAllLiabilityCoverages()
            {
                _logger.LogInformation("Controller activated to list all liability coverages...");

                try
                {
                    var liabilityCoverages = _unitOfWork.LiabilityCoverages.GetLiabilityCoverage();
                    _logger.LogInformation("Liability coverages retrieved succesfully: {LiabilityCoveragesCount}",
                                           liabilityCoverages.Result.Count);

                    return (true, "Liability coverages retrieved successfully.", liabilityCoverages.Result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while fetching liability coverages.");
                    return (false, "An error occurred while fetching the liability coverages.", new List<LiabilityCoverage>());
                }

            }
            
            // This method fetches all property and inventory coverages from the database. The list of data will be used in the view for listing all property and inventory coverages.
            public async Task<(bool Success, string Message, List<PropertyAndInventoryCoverage> PropertyAndInventoryCoverages)> GetAllPropertyAndInventoryCoverages()
            {
                _logger.LogInformation("Controller activated to list all property and inventory coverages...");

                try
                {
                    var propertyAndInventoryCoverages = _unitOfWork.PropertyAndInventoryCoverages.GetAllPropertyAndInventoryCoverages();
                    _logger.LogInformation("Property and inventory coverages retrieved succesfully: {PropertyAndInventoryCoveragesCount}",
                                                              propertyAndInventoryCoverages.Result.Count);

                    return (true, "Property and inventory coverages retrieved successfully.", propertyAndInventoryCoverages.Result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while fetching property and inventory coverages.");
                    return (false, "An error occurred while fetching the property and inventory coverages.", new List<PropertyAndInventoryCoverage>());
                }
            }

        }
    
}
