using SU.Backend.Models.Customers;

namespace SU.Backend.Database.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the PrivateCustomerRepository class must implement.
/// </summary>
public interface IPrivateCustomerRepository
{
    Task<PrivateCustomer> GetPrivateCustomerById(PrivateCustomer privateCustomer);
    Task<List<PrivateCustomer>> GetAllPrivateCustomers();

    Task<List<PrivateCustomer>> GetProspectDataForPrivateCustomers();
}