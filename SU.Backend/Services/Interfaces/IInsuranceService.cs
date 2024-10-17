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
    public interface IInsuranceService
    {

        //Implementation 

        //Privatförsäkring
        Task<(bool Success, string Message)> CreatePrivateInsurance(
            PrivateCustomer privateCustomer,
            InsuranceType insuranceType,
            PrivateCoverageOption privateCoverageOption,
            Employee seller,
            bool isPolicyHolderInsured,
            string? note,
            DateTime? startDate = null,
            DateTime? endDate = null,
            List<InsuranceAddonType>? addons = null, // Nullable lista med addons
            InsuredPerson? insuredPerson = null);

        //Företagsförsäkring: Fastighet & Inventarie
        Task<(bool Success, string Message)> CreatePropertyInventoryInsurance(
                    CompanyCustomer companyCustomer,
                    PropertyAndInventoryCoverage propertyAndInventoryCoverage,
                    Employee seller,
                    string note,
                    DateTime? startDate = null, 
                    DateTime? endDate = null    
        );

        //Företagsförsäkring: Ansvar
        Task<(bool Success, string Message)> CreateLiabilityInsurance(
                    CompanyCustomer companyCustomer,
                    LiabilityCoverage liabilityCoverage,
                    Employee seller,
                    string note,
                    DateTime? startDate = null,
                    DateTime? endDate = null
        );

        //Företagsförsäkring: Fordon
        Task<(bool Success, string Message)> CreateVehicleInsurance(
                    CompanyCustomer companyCustomer,
                    VehicleInsuranceCoverage vehicleCoverage,
                    Employee seller,
                    string note,
                    DateTime? startDate = null,
                    DateTime? endDate = null
        );

        Task<(bool Success, string Message)> DeleteInsurance(
           Insurance insurance);

        //Tests
        Task<(bool Success, string Message)> CreateTestPrivateInsurance();

        Task<(bool Success, string Message)> CreateTestCompanyInsurance();

        Task<(bool Success, string Message)> CreateTestCompanyInsuranceProperty();

        Task<(bool Success, string Message)> CreateTestCompanyLiability();

        //Lists
        Task<(bool Success, string Message, List<InsuranceAddonType> InsuranceAddonTypes)> GetAllInsuranceAddonTypes();

        Task<(bool Success, string Message, List<InsuranceAddon> InsuranceAddons)> GetAllInsuranceAddons();

        Task<(bool Success, string Message, List<InsurancePolicyHolder> InsurancePolicyHolders)> GetAllInsurancePolicyHolders();

        Task<(bool Success, string Message, List<Insurance> Insurances)> GetAllInsurances();

        Task<(bool Success, string Message, List<InsuranceCoverage> InsuranceCoverages)> GetAllInsuranceCoverages();

        Task<(bool Success, string Message, List<VehicleInsuranceCoverage> VehicleInsuranceCoverages)> GetAllVehicleInsuranceCoverages();

        Task<(bool Success, string Message, List<VehicleInsuranceOption> VehicleInsuranceCoverages)> GetAllVehicleInsuranceOptions();

        Task<(bool Success, string Message, List<Riskzone> Riskzones)> GetAllRiskzones();

        Task<(bool Success, string Message, List<LiabilityCoverageOption> LiabilityCoverageOptions)> GetAllLiabilityCoverageOptions();

        Task<(bool Success, string Message, List<LiabilityCoverage> LiabilityCoverages)> GetAllLiabilityCoverages();
    }
}
