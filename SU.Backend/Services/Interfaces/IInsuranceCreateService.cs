﻿using SU.Backend.Models.Customers;
using SU.Backend.Models.Insurances.Coverage;
using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Enums.Insurance.Addons;
using SU.Backend.Models.Employees;

namespace SU.Backend.Services.Interfaces
{
    /// <summary>
    /// This interface is responsible for defining the methods that the InsuranceCreateService class must implement.
    /// </summary>
    public interface IInsuranceCreateService
    {
        //Implementation 

        //Private Insurance
        Task<(bool Success, string Message)> CreatePrivateInsurance(
            PrivateCustomer privateCustomer,
            InsuranceType insuranceType,
            PrivateCoverageOption privateCoverageOption,
            Employee seller,
            bool isPolicyHolderInsured,
            string? note,
            PaymentPlan paymentPlan,
            DateTime? startDate = null,
            DateTime? endDate = null,
            List<InsuranceAddonType>? addons = null, // Nullable lista med addons
            InsuredPerson? insuredPerson = null
            );

        //Company Insurance: Property and Inventory
        Task<(bool Success, string Message)> CreatePropertyInventoryInsurance(
                    CompanyCustomer companyCustomer,
                    PropertyAndInventoryCoverage propertyAndInventoryCoverage,
                    Employee seller,
                    string note,
                    PaymentPlan paymentPlan,
                    DateTime? startDate = null, 
                    DateTime? endDate = null    
        );

        // Company Insurance: Liability
        Task<(bool Success, string Message)> CreateLiabilityInsurance(
                    CompanyCustomer companyCustomer,
                    LiabilityCoverage liabilityCoverage,
                    Employee seller,
                    string note,
                    PaymentPlan paymentPlan,
                    DateTime? startDate = null,
                    DateTime? endDate = null
        );

        // Company Insurance: Vehicle
        Task<(bool Success, string Message)> CreateVehicleInsurance(
                    CompanyCustomer companyCustomer,
                    VehicleInsuranceCoverage vehicleCoverage,
                    Employee seller,
                    string note,
                    PaymentPlan paymentPlan,
                    DateTime? startDate = null,
                    DateTime? endDate = null
        );

        Task<(bool Success, string Message)> DeleteInsurance(
           Insurance insurance);

        Task<(bool Success, string Message)> UpdateInsurance(
           Insurance insurance);






    }
}
