using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Customers;

namespace SU.Backend.Database.Repositories;

/// <summary>
///     This class is responsible for implementing the methods defined in the IPrivateCustomerRepository interface.
/// </summary>
public class PrivateCustomerRepository : Repository<PrivateCustomer>, IPrivateCustomerRepository
{
    public PrivateCustomerRepository(Context context) : base(context)
    {
    }

    public async Task<PrivateCustomer> GetPrivateCustomerById(PrivateCustomer privateCustomer)
    {
        return await _context.PrivateCustomers.FirstOrDefaultAsync(x =>
            x.PrivateCustomerId == privateCustomer.PrivateCustomerId);
    }

    // This method is used to get the data of the private customers who have only one insurance policy.
    // This is the definition of a prospect according to the business documentation.
    public async Task<List<PrivateCustomer>> GetProspectDataForPrivateCustomers()
    {
        return _context.PrivateCustomers
            .Where(x => x.InsurancePolicyHolders.Count == 1)
            .Include(i => i.InsurancePolicyHolders)
            .ThenInclude(p => p.Insurance)
            .ToList();
    }

    public async Task<List<PrivateCustomer>> GetAllPrivateCustomers()
    {
        return _context.PrivateCustomers
            .Include(c => c.InsurancePolicyHolders)
            .ThenInclude(p => p.Insurance)
            .ThenInclude(s => s.Seller)
            .ToList();
    }
}