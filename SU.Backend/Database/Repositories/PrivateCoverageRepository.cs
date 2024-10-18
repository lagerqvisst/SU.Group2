using Microsoft.EntityFrameworkCore;
using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    /// <summary>
    /// This class is responsible for implementing the methods defined in the IPrivateCoverageRepository interface.
    /// </summary>
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
