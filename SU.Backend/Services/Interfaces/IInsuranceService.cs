using SU.Backend.Models.Customers;
using SU.Backend.Models.Insurance.Coverage;
using SU.Backend.Models.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    public interface IInsuranceService
    {
        Task<(bool Success, string Message)> CreateInsurance();
        Task<(bool Success, string Message)> CreateInsurancePolicyHolder(PrivateCustomer? privateCustomer, CompanyCustomer? companyCustomer);
        Task<(bool Success, string Message)> CreateInsuranceCoverage(Insurance insurance);
        Task<(bool Success, string Message)> CreatePrivateInsuranceCoverage(InsuranceCoverage insuranceCoverage, PrivateCoverageOption privateCoverageOption, InsuredPerson insuredPerson);
        Task<(bool Success, string Message)> CreateInsuredPerson(string name, string personalNumber, PrivateCoverage privateCoverage);
    }
}
