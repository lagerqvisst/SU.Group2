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
    /// This class is responsible for implementing the methods defined in the IVehicleInsuranceCoverageRepository interface.
    /// </summary>
    public class VehicleInsuranceCoverageRepository : Repository<VehicleInsuranceCoverage>, IVehicleInsuranceCoverageRepository
    {
        public VehicleInsuranceCoverageRepository(Context context) : base(context)
        {
        }

        public async Task<List<VehicleInsuranceCoverage>> GetAllVehicleInsuranceCoverages()
        {
            return await _context.VehicleInsuranceCoverages.ToListAsync();
        }
    }
}
