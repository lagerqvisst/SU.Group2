using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Interfaces
{
    public interface IVehicleInsuranceOptionRepository
    {
        Task<List<VehicleInsuranceOption>> GetVehicleInsuranceOptions();

    }
}
