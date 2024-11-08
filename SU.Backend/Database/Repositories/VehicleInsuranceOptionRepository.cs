using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Database.Repositories;

/// <summary>
///     This class is responsible for implementing the methods defined in the IVehicleInsuranceOptionRepository interface.
/// </summary>
public class VehicleInsuranceOptionRepository : Repository<VehicleInsuranceOption>, IVehicleInsuranceOptionRepository
{
    public VehicleInsuranceOptionRepository(Context context) : base(context)
    {
    }

    //This method is used to get all vehicle insurance options
    public async Task<List<VehicleInsuranceOption>> GetVehicleInsuranceOptions()
    {
        return _context.VehicleInsuranceOptions.ToList();
    }
}