using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    public interface ICompanyCustomerService
    {
        Task<(bool Success, string Message, CompanyCustomer Customer)> GenerateTestCompanyCustomer();

        Task<(bool Success, string Message)> CreateCompanyCustomer(CompanyCustomer newCompanyCustomer);

        Task<(bool Success, string Message)> UpdateCompanyCustomer(CompanyCustomer companyCustomer);

        Task<(bool Success, string Message)> DeleteCompanyCustomer(CompanyCustomer companyCustomer);

        //Used for updating and deleting specific company customer.
        Task<(bool Success, string Message, CompanyCustomer? Customer)> GetCompanyCustomerById(int id);

        Task<(bool Success, string Message, List<CompanyCustomer> CompanyCustomers)> ListAllCompanyCustomers();

    }

}

