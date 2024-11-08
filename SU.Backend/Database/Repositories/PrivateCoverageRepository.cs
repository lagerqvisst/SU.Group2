using Microsoft.EntityFrameworkCore;
using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Database.Repositories;

/// <summary>
///     This class is responsible for implementing the methods defined in the IPrivateCoverageRepository interface.
/// </summary>
public class PrivateCoverageRepository : Repository<PrivateCoverage>
{
    public PrivateCoverageRepository(Context context) : base(context)
    {
    }

    //This method is used to get all private coverages
    public async Task<List<PrivateCoverage>> GetAllPrivateCoverages()
    {
        return await _context.PrivateCoverages.ToListAsync();
    }
}