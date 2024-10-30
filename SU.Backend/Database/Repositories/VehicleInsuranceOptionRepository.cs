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
    /// This class is responsible for implementing the methods defined in the IVehicleInsuranceOptionRepository interface.
    /// </summary>
    public class VehicleInsuranceOptionRepository : Repository<VehicleInsuranceOption>, IVehicleInsuranceOptionRepository
    {
        public VehicleInsuranceOptionRepository(Context context) : base(context)
        {
        }

        public async Task<List<VehicleInsuranceOption>> GetVehicleInsuranceOptions()
        {
            return _context.VehicleInsuranceOptions.ToList();
        }
    }
}
