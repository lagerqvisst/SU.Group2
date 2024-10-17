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
    public class InsuranceAddonTypeRepository : Repository<InsuranceAddonType>, IInsuranceAddonTypeRepository
    {
        public InsuranceAddonTypeRepository(Context context) : base(context)
        {
        }

        public async Task <List<InsuranceAddonType>> ListAllInsuranceAddonTypes()
        {
            return await _context.InsuranceAddonTypes.ToListAsync();
        }

        public async Task<InsuranceAddonType> GetSpecificAddonType(AddonType addonType, decimal coverageAmount)
        {
            return await _context.InsuranceAddonTypes
                .FirstOrDefaultAsync(x => x.Description == addonType && x.CoverageAmount == coverageAmount);
        }
    }
}
