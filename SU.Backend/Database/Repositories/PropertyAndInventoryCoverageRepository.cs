using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Database.Repositories;

/// <summary>
///     This class is responsible for implementing the methods defined in the IPropertyAndInventoryCoverageRepository
///     interface.
/// </summary>
public class PropertyAndInventoryCoverageRepository : Repository<PropertyAndInventoryCoverage>,
    IPropertyAndInventoryCoverageRepository
{
    public PropertyAndInventoryCoverageRepository(Context context) : base(context)
    {
    }

    // This method is used to get all property and inventory coverages
    public async Task<List<PropertyAndInventoryCoverage>> GetAllPropertyAndInventoryCoverages()
    {
        return await _context.PropertyAndInventoryCoverages.ToListAsync();
    }
}