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
    /// <summary>
    /// This class is responsible for implementing the methods defined in the IRiskzoneRepository interface.
    /// </summary>
    public class RiskzoneRepository : Repository<RiskZone>, IRiskzoneRepository
    {
        public RiskzoneRepository(Context context) : base(context)
        {
        }

        public async Task<List<RiskZone>> GetAllRiskZones()
        {
            return await _context.Riskzones.ToListAsync();
        }
    }
}
