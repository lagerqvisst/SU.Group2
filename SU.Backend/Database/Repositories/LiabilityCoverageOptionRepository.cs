using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Database.Repositories;

/// <summary>
///     This class is responsible for implementing the methods defined in the ILiabilityCoverageOptionRepository interface.
/// </summary>
public class LiabilityCoverageOptionRepository : Repository<LiabilityCoverageOptionRepository>,
    ILiabilityCoverageOptionRepository
{
    public LiabilityCoverageOptionRepository(Context context) : base(context)
    {
    }

    public async Task<List<LiabilityCoverageOption>> GetLiabilityCoverageOptions()
    {
        return _context.LiabilityCoverageOption.ToList();
    }
}