using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Customers;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public class PrivateCustomerRepository : Repository<PrivateCustomer>, IPrivateCustomerRepository
    {
        public PrivateCustomerRepository(Context context) : base(context)
        {
        }

        public async Task<PrivateCustomer> GetPrivateCustomerById(PrivateCustomer privateCustomer)
        {
            return await _context.PrivateCustomers.FirstOrDefaultAsync(x => x.PrivateCustomerId == privateCustomer.PrivateCustomerId);
        }

        public async Task<List<PrivateCustomer>> GetPrivateCustomers() 
        {
            return _context.PrivateCustomers.ToList();
        }

        public async Task<List<PrivateCustomer>> GetProspectDataForPrivateCustomers()
        {
            return await _context.PrivateCustomers
                .Where(x => x.InsurancePolicyHolders.Count == 1)
                .ToListAsync();
        }

        public async Task<List<PrivateCustomer>> ListAllPrivateCustomers()
        {
            return await _context.PrivateCustomers.ToListAsync();
        }
    }

}
