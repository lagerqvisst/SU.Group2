using Microsoft.EntityFrameworkCore;
using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public class PrivateCoverageRepository : Repository<PrivateCoverage>
    {
        public PrivateCoverageRepository(Context context) : base(context)
        {
        }

        public async Task<List<PrivateCoverage>> GetAllPrivateCoverages()
        {
            return await _context.PrivateCoverages.ToListAsync();
        }
    }

}
