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
        Task<(bool Success, string Message)> CreatePrivateInsurance(
            PrivateCustomer privateCustomer,
            InsuranceType insuranceType,
            PrivateCoverageOption privateCoverageOption,
            Employee seller,
            bool isPolicyHolderInsured,
            InsuredPerson? insuredPerson = null);

        Task<(bool Success, string Message)> DeleteInsurance(
           Insurance insurance);

        //Tests
        Task<(bool Success, string Message)> CreateTestPrivateInsurance();

        Task<(bool Success, string Message)> CreateTestCompanyInsurance();

        Task<(bool Success, string Message)> CreateTestCompanyInsuranceProperty();

        Task<(bool Success, string Message)> CreateTestCompanyLiability();

        Task<(bool Success, string Message)> RemoveAllInsurances();

    }
}
