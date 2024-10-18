using SU.Backend.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    /// <summary>
    /// This interface is responsible for defining the methods that the PrivateCustomerService class must implement.
    /// </summary>
    public interface IPrivateCustomerService
    {
        Task<(bool Success, string Message, PrivateCustomer Customer)> GenerateRandomPrivateCustomer();
        Task<(bool Success, string Message, PrivateCustomer Customer)> GetPrivateCustomerById(PrivateCustomer privateCustomer);
        Task<(bool Success, string Message)> CreateNewPrivateCustomer(PrivateCustomer privateCustomer);
        Task<(bool Success, string Message)> DeletePrivateCustomer(PrivateCustomer privateCustomer);
        Task<(bool Success, string Message)> UpdatePrivateCustomer(PrivateCustomer privateCustomer);
        Task<(bool Success, string Message, List<PrivateCustomer> PrivateCustomers)> GetAllPrivateCustomers();

    }
}
