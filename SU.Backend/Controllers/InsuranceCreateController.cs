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
    /// This class is responsible for handling the insurance create controller.
    /// Makes logic available in the Viewmodel
    /// More info about the logic for each method can be found in the Service function each controller method uses.
    /// </summary>
    public class InsuranceCreateController
    {
        private readonly IInsuranceCreateService _InsuranceCreateService; 
        private readonly ILogger<InsuranceCreateController> _logger;

        public InsuranceCreateController(IInsuranceCreateService InsuranceCreateService, ILogger<InsuranceCreateController> logger)
        {
            _InsuranceCreateService = InsuranceCreateService;
            _logger = logger;
        }

        public async Task<(bool Success, string Message)> CreatePrivateInsurance(PrivateCustomer privateCustomer,
            InsuranceType insuranceType,
            PrivateCoverageOption privateCoverageOption,
            Employee seller,
            bool isPolicyHolderInsured,
            string? note,
            PaymentPlan paymentPlan,
            DateTime? startDate = null,
            DateTime? endDate = null,
            List<InsuranceAddonType>? addons = null, // Nullable list with addons
            InsuredPerson? insuredPerson = null)
        {
            _logger.LogInformation("Controller activated to create new private insurance...");
            var result = await _InsuranceCreateService.CreatePrivateInsurance(privateCustomer, insuranceType, privateCoverageOption, seller, isPolicyHolderInsured, note, paymentPlan, startDate, endDate, addons, insuredPerson);

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
            PropertyAndInventoryCoverage propertyAndInventoryCoverage, Employee seller, string note, PaymentPlan paymentPlan)
        {
            _logger.LogInformation("Controller activated to create new property and inventory insurance...");
            var result = await _InsuranceCreateService.CreatePropertyInventoryInsurance(companyCustomer, propertyAndInventoryCoverage, seller, note, paymentPlan);

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
            LiabilityCoverage liabilityCoverage, Employee seller, string note, PaymentPlan paymentPlan)
        {
            _logger.LogInformation("Controller activated to create new liability insurance...");
            var result = await _InsuranceCreateService.CreateLiabilityInsurance(companyCustomer, liabilityCoverage, seller, note, paymentPlan);

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
            VehicleInsuranceCoverage vehicleCoverage, Employee seller, string note, PaymentPlan paymentPlan)
        {
            _logger.LogInformation("Controller activated to create new vehicle insurance...");
            var result = await _InsuranceCreateService.CreateVehicleInsurance(companyCustomer, vehicleCoverage, seller, note, paymentPlan);

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

       public async Task<(bool Success, string Message)> DeleteInsurance(Insurance insurance)
        {
            _logger.LogInformation("Controller activated to delete insurance...");
            var result = await _InsuranceCreateService.DeleteInsurance(insurance);

            if (result.Success)
            {
                _logger.LogInformation($"Insurance deleted successfully:\n{result.Message}");
                return (result.Success, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error deleting insurance: {result.Message}");
                return (result.Success, result.Message);
            }
        }

        public async Task<(bool Success, string Message)> UpdateInsurance(Insurance insurance)
        {
            _logger.LogInformation("Controller activated to update insurance...");
            var result = await _InsuranceCreateService.UpdateInsurance(insurance);

            if (result.Success)
            {
                _logger.LogInformation($"Insurance updated successfully:\n{result.Message}");
                return (result.Success, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error updating insurance: {result.Message}");
                return (result.Success, result.Message);
            }
        }
    }
}

