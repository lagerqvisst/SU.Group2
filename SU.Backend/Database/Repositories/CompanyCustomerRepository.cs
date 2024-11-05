using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Customers;

namespace SU.Backend.Database.Repositories;

/// <summary>
///     This class is responsible for implementing the methods defined in the IPrivateCustomerRepository interface.
/// </summary>
public class CompanyCustomerRepository : Repository<CompanyCustomer>, ICompanyCustomerRepository
{
    public CompanyCustomerRepository(Context context) : base(context)
    {
    }

    //Used for getting all company customers that have only one insurance policy holder.
    //Used in logic for getting prospect data for company customers.
    public async Task<List<CompanyCustomer>> GetProspectDataForCompanyCustomers()
    {
        return await _context.CompanyCustomers
            .Where(x => x.InsurancePolicyHolders.Count == 1)
            .Include(i => i.InsurancePolicyHolders)
            .ThenInclude(p => p.Insurance)
            .ThenInclude(s => s.Seller)
            .ToListAsync();
    }

    //Used for udating and deleting specific company customer.
    public async Task<CompanyCustomer?> GetCompanyCustomerById(int id)
    {
        return await _context.CompanyCustomers.FirstOrDefaultAsync(x => x.CompanyCustomerId == id);
    }

    public async Task<List<CompanyCustomer>> GetAllCompanyCustomers()
    {
        return _context.CompanyCustomers
            .Include(c => c.InsurancePolicyHolders)
            .ThenInclude(p => p.Insurance)
            .ToList();
    }


    public async Task<List<CompanyCustomer>> CreateCompanyCustomers(CompanyCustomer CompanyCustomer)
    {
        await _context.CompanyCustomers.AddAsync(CompanyCustomer);
        await _context.SaveChangesAsync();
        return await _context.CompanyCustomers.ToListAsync();
    }
}