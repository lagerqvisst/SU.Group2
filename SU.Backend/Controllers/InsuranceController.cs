﻿using Microsoft.Extensions.Logging;
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
    public class InsuranceController
    {
        private readonly IInsuranceService _insuranceService; 
        private readonly ILogger<InsuranceController> _logger;

        public InsuranceController(IInsuranceService insuranceService, ILogger<InsuranceController> logger)
        {
            _insuranceService = insuranceService;
            _logger = logger;
        }

        public async Task<(List<InsuranceAddonType> insuranceAddonTypes, string Message)> ListAllInsuranceAddonTypes()
        {
            _logger.LogInformation("Controller activated to list all insurance addon types...");
            var result = await _insuranceService.ListAllInsuranceAddonTypes();

            if (result.Success) 
            {
                _logger.LogInformation($"Insurance addon types retrieved succesfully:\n{result.Message}");
                return (result.insuranceAddonTypes, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving insurance addon types: {result.Message}");
                return (new List<InsuranceAddonType>(), result.Message);
            }
        }

        public async Task CreatePrivateInsurance(PrivateCustomer privateCustomer,
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
            }
            else
            {
                _logger.LogWarning($"Error creating private insurance: {result.Message}");
            }
        }

        public async Task CreatePropertyInventoryInsurance(CompanyCustomer companyCustomer,
            PropertyAndInventoryCoverage propertyAndInventoryCoverage, Employee seller, string note)
        {
            _logger.LogInformation("Controller activated to create new property and inventory insurance...");
            var result = await _insuranceService.CreatePropertyInventoryInsurance(companyCustomer, propertyAndInventoryCoverage, seller, note);

            if (result.Success)
            {
                _logger.LogInformation($"Property and inventory insurance created successfully:\n{result.Message}");
            }
            else
            {
                _logger.LogWarning($"Error creating property and inventory insurance: {result.Message}");
            }
        }
    }
}

