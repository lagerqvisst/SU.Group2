using SU.Backend.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Interfaces
{
    public interface ICompanyCustomerRepository
    {
        Task<List<CompanyCustomer>> GetProspectDataForCompanyCustomers();

        Task<List<CompanyCustomer>> GetCompanyCustomers();
    }
}
