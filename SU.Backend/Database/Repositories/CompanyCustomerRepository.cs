using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    /// <summary>
    /// This class is responsible for implementing the methods defined in the IPrivateCustomerRepository interface.
    /// </summary>
    public class CompanyCustomerRepository : Repository<CompanyCustomer>, ICompanyCustomerRepository
    {
        public CompanyCustomerRepository(Context context) : base(context)
        {
        }


        public async Task<List<CompanyCustomer>> CreateCompanyCustomers(CompanyCustomer CompanyCustomer)
        {
            await _context.CompanyCustomers.AddAsync(CompanyCustomer);
            await _context.SaveChangesAsync();  
            return await _context.CompanyCustomers.ToListAsync(); 
        }

        //Used for getting all company customers that have only one insurance policy holder.
        //Used in logic for getting prospect data for company customers.
        public async Task<List<CompanyCustomer>> GetProspectDataForCompanyCustomers()
        {
            return await _context.CompanyCustomers
                .Where(x => x.InsurancePolicyHolders.Count == 1)
                .ToListAsync();
        }

        //Used for udating and deleting specific company customer.
        public async Task<CompanyCustomer?> GetCompanyCustomerById (int id)
        {
            return await _context.CompanyCustomers.FirstOrDefaultAsync(x => x.CompanyCustomerId == id);
        }

        public async Task <List<CompanyCustomer>> GetAllCompanyCustomers()
        {
            return await _context.CompanyCustomers.ToListAsync();
        }
    }
}
