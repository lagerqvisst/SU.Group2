using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Enums.Insurance.Addons;
using SU.Backend.Models.Insurances;
using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public class InsuranceAddonRepository : Repository<InsuranceAddon>, IInsuranceAddonRepository
    {
        public InsuranceAddonRepository(Context context) : base(context)
        {
        }
        public async Task<List<InsuranceAddon>> GetAllInsuranceAddons()
        {
            return await _context.InsuranceAddons.ToListAsync();
        }
    }
}