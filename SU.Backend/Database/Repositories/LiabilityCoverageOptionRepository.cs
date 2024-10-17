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
    public class LiabilityCoverageOptionRepository : Repository<LiabilityCoverageOptionRepository>, ILiabilityCoverageOptionRepository
    {
        public LiabilityCoverageOptionRepository(Context context) : base(context)
        {
        }

        public async Task<List<LiabilityCoverageOption>> GetLiabilityCoverageOptions()
        {
            return await _context.LiabilityCoverageOption.ToListAsync();
        }
    }
}
