using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Insurances.Prospects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    /// <summary>
    /// This class is responsible for implementing the methods defined in the IProspectRepository interface.
    /// </summary>
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
                .Include(e => e.Seller)
                .ToListAsync();
        }

        // This method checks if a prospect exists in the database
        // Used in Prospect service to prevent duplicates
        public async Task<bool> ProspectExists(int? privateCustomerId, int? companyCustomerId)
        {
            // If both privateCustomerId and companyCustomerId are null, return false
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
