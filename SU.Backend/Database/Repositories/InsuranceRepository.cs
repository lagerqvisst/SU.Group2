using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public class InsuranceRepository : Repository<Insurance>, IIunsuranceRepository
    {
        public InsuranceRepository(Context context) : base(context)
        {
        }

        public async Task<List<Insurance>> GetAllInsurances()
        {
            return await _context.Insurances
                .Include(i => i.InsurancePolicyHolder)
                    .ThenInclude(p => p.PrivateCustomer)
                .Include(i => i.InsuranceCoverage)
                    .ThenInclude(ic => ic.PrivateCoverage)
                        .ThenInclude(pc => pc.PrivateCoverageOption) // Inkludera PrivateCoverageOption
                .Include(i => i.InsuranceCoverage)
                    .ThenInclude(ic => ic.PrivateCoverage)
                        .ThenInclude(pc => pc.InsuredPerson) // Inkludera InsuredPerson
                .Include(i => i.InsuranceAddons)
                .ToListAsync();
        }

    }

}
