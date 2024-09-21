using SU.Backend.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Interfaces
{
    public interface IPrivateCustomerRepository
    {
        Task<PrivateCustomer> GetPrivateCustomerById(PrivateCustomer privateCustomer);
        Task<List<PrivateCustomer>> GetPrivateCustomers();
    }
}
