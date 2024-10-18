using SU.Backend.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Interfaces
{
    /// <summary>
    /// This interface is responsible for defining the methods that the CompanyCustomerRepository class must implement.
    /// </summary>
    public interface ICompanyCustomerRepository
    {
        Task<List<CompanyCustomer>> GetProspectDataForCompanyCustomers();

        Task<CompanyCustomer?> GetCompanyCustomerById(int id);

        Task<List<CompanyCustomer>> GetAllCompanyCustomers();
    }
}
