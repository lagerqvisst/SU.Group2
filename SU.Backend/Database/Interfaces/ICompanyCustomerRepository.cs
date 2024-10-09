using SU.Backend.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Interfaces
{
    public interface ICompanyCustomerRepository

        //All query definitions for CompanyCustomer
    {
        Task<List<CompanyCustomer>> GetProspectDataForCompanyCustomers();

        Task<List<CompanyCustomer>> GetCompanyCustomers();

        Task<CompanyCustomer?> GetCompanyCustomerById(int id);
    }
}
