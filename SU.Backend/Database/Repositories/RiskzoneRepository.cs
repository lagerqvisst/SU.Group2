using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Database.Repositories;

/// <summary>
///     This class is responsible for implementing the methods defined in the IRiskzoneRepository interface.
/// </summary>
public class RiskzoneRepository : Repository<RiskZone>, IRiskzoneRepository
{
    public RiskzoneRepository(Context context) : base(context)
    {
    }

    //This method is used to get all risk zones
    public async Task<List<RiskZone>> GetAllRiskZones()
    {
        return _context.Riskzones.ToList();
    }
}