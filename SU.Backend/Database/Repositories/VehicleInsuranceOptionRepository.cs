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
    public class VehicleInsuranceOptionRepository : Repository<VehicleInsuranceOption>, IVehicleInsuranceOptionRepository
    {
        public VehicleInsuranceOptionRepository(Context context) : base(context)
        {
        }

        public async Task<List<VehicleInsuranceOption>> GetVehicleInsuranceOptions()
        {
            return await _context.VehicleInsuranceOptions.ToListAsync();
        }
    }
}
