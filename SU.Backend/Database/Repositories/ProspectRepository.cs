using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Insurance.Prospects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public class ProspectRepository : Repository<Prospect>, IProspectRepository
    {
        public ProspectRepository(Context context) : base(context)
        {
        }

        public async Task<List<Prospect>> GetAllProspects()
        {

            return await _context.Prospects
                .Include(p => p.PrivateCustomer)
                .Include(p => p.CompanyCustomer)
                .Include(e => e.Employee)
                .ToListAsync();
        }

        public async Task<bool> ProspectExists(int? privateCustomerId, int? companyCustomerId)
        {
            // Om båda är null, finns ingen prospect
            if (privateCustomerId == null && companyCustomerId == null)
            {
                return false;
            }

            return await _context.Prospects
                .AnyAsync(p => (privateCustomerId != null && p.PrivateCustomerId == privateCustomerId) ||
                                (companyCustomerId != null && p.CompanyCustomerId == companyCustomerId));
        }


    }
}
