using SU.Backend.Models.Customers;
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
        Task<(bool success, string message)> CreatePrivateInsurance(
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
        Task<(bool success, string message)> CreatePropertyInventoryInsurance(
                    CompanyCustomer companyCustomer,
                    PropertyAndInventoryCoverage propertyAndInventoryCoverage,
                    Employee seller,
                    string note,
                    PaymentPlan paymentPlan,
                    DateTime? startDate = null, 
                    DateTime? endDate = null    
        );

        // Company Insurance: Liability
        Task<(bool success, string message)> CreateLiabilityInsurance(
                    CompanyCustomer companyCustomer,
                    LiabilityCoverage liabilityCoverage,
                    Employee seller,
                    string note,
                    PaymentPlan paymentPlan,
                    DateTime? startDate = null,
                    DateTime? endDate = null
        );

        // Company Insurance: Vehicle
        Task<(bool success, string message)> CreateVehicleInsurance(
                    CompanyCustomer companyCustomer,
                    VehicleInsuranceCoverage vehicleCoverage,
                    RiskZone riskZone,
                    Employee seller,
                    string note,
                    PaymentPlan paymentPlan,
                    DateTime? startDate = null,
                    DateTime? endDate = null
        );

        Task<(bool success, string message)> DeleteInsurance(
           Insurance insurance);

        Task<(bool success, string message)> UpdateInsurance(
           Insurance insurance);






    }
}
