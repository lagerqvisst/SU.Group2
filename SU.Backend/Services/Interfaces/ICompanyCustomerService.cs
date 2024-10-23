using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    /// <summary>
    /// This interface is responsible for defining the methods that the CompanyCustomerService class must implement.
    /// </summary>
    public interface ICompanyCustomerService
    {
        Task<(bool success, string message, CompanyCustomer customer)> GenerateTestCompanyCustomer();

        Task<(bool success, string message)> CreateCompanyCustomer(CompanyCustomer newCompanyCustomer);

        Task<(bool success, string message)> UpdateCompanyCustomer(CompanyCustomer companyCustomer);

        Task<(bool success, string message)> DeleteCompanyCustomer(CompanyCustomer companyCustomer);

        //Used for updating and deleting specific company customer.
        Task<(bool success, string message, CompanyCustomer? customer)> GetCompanyCustomerById(int id);

        Task<(bool Success, string Message, List<CompanyCustomer> CompanyCustomers)> GetAllCompanyCustomers();

    }

}

