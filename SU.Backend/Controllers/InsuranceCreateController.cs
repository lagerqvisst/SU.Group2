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
        private readonly IInsuranceCreateService _insuranceCreateService; 
        private readonly ILogger<InsuranceCreateController> _logger;

        public InsuranceCreateController(IInsuranceCreateService insuranceCreateService, ILogger<InsuranceCreateController> logger)
        {
            _insuranceCreateService = insuranceCreateService;
            _logger = logger;
        }

        public async Task<(bool success, string message)> CreatePrivateInsurance(PrivateCustomer privateCustomer,
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
            var result = await _insuranceCreateService.CreatePrivateInsurance(privateCustomer, insuranceType, privateCoverageOption, seller, isPolicyHolderInsured, note, paymentPlan, startDate, endDate, addons, insuredPerson);

            if (result.success)
            {
                _logger.LogInformation($"Private insurance created successfully:\n{result.message}");
                return (result.success, result.message);
            }
            else
            {
                _logger.LogWarning($"Error creating private insurance: {result.message}");
                return (result.success, result.message);
            }
        }

        public async Task<(bool success, string message)> CreatePropertyInventoryInsurance(CompanyCustomer companyCustomer,
            PropertyAndInventoryCoverage propertyAndInventoryCoverage, Employee seller, string note, PaymentPlan paymentPlan)
        {
            _logger.LogInformation("Controller activated to create new property and inventory insurance...");
            var result = await _insuranceCreateService.CreatePropertyInventoryInsurance(companyCustomer, propertyAndInventoryCoverage, seller, note, paymentPlan);

            if (result.success)
            {
                _logger.LogInformation($"Property and inventory insurance created successfully:\n{result.message}");
                return (result.success, result.message);
            }
            else
            {
                _logger.LogWarning($"Error creating property and inventory insurance: {result.message}");
                return (result.success, result.message);
            }
        }

        public async Task<(bool success, string message)> CreateLiabilityInsurance(CompanyCustomer companyCustomer,
            LiabilityCoverage liabilityCoverage, Employee seller, string note, PaymentPlan paymentPlan)
        {
            _logger.LogInformation("Controller activated to create new liability insurance...");
            var result = await _insuranceCreateService.CreateLiabilityInsurance(companyCustomer, liabilityCoverage, seller, note, paymentPlan);

            if (result.success)
            {
                _logger.LogInformation($"Liability insurance created successfully:\n{result.message}");
                return (result.success, result.message);
            }
            else
            {
                _logger.LogWarning($"Error creating liability insurance: {result.message}");
                return (result.success, result.message);
            }
        }

        public async Task<(bool success, string message)> CreateVehicleInsurance(CompanyCustomer companyCustomer,
            VehicleInsuranceCoverage vehicleCoverage, Employee seller, string note, PaymentPlan paymentPlan)
        {
            _logger.LogInformation("Controller activated to create new vehicle insurance...");
            var result = await _insuranceCreateService.CreateVehicleInsurance(companyCustomer, vehicleCoverage, seller, note, paymentPlan);

            if (result.success)
            {
                _logger.LogInformation($"Vehicle insurance created successfully:\n{result.message}");
                return (result.success, result.message);
            }
            else
            {
                _logger.LogWarning($"Error creating vehicle insurance: {result.message}");
                return (result.success, result.message);
            }
        }

       public async Task<(bool success, string message)> DeleteInsurance(Insurance insurance)
        {
            _logger.LogInformation("Controller activated to delete insurance...");
            var result = await _insuranceCreateService.DeleteInsurance(insurance);

            if (result.success)
            {
                _logger.LogInformation($"Insurance deleted successfully:\n{result.message}");
                return (result.success, result.message);
            }
            else
            {
                _logger.LogWarning($"Error deleting insurance: {result.message}");
                return (result.success, result.message);
            }
        }

        public async Task<(bool success, string message)> UpdateInsurance(Insurance insurance)
        {
            _logger.LogInformation("Controller activated to update insurance...");
            var result = await _insuranceCreateService.UpdateInsurance(insurance);

            if (result.success)
            {
                _logger.LogInformation($"Insurance updated successfully:\n{result.message}");
                return (result.success, result.message);
            }
            else
            {
                _logger.LogWarning($"Error updating insurance: {result.message}");
                return (result.success, result.message);
            }
        }
    }
}

