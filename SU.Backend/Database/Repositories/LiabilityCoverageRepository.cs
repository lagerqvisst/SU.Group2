using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public class LiabilityCoverageRepository : Repository<LiabilityCoverage>, ILiabilityCoverageRepository
    {
        public LiabilityCoverageRepository(Context context) : base(context)
        {
        }

        public async Task<List<LiabilityCoverage>> GetLiabilityCoverage()
        {
            return await _context.LiabilityCoverages.ToListAsync();
        }
    }
}
