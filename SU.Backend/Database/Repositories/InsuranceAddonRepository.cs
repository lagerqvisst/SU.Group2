using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Insurances;

namespace SU.Backend.Database.Repositories;

/// <summary>
///     This class is responsible for implementing the methods defined in the IInsuranceAddonRepository interface.
/// </summary>
public class InsuranceAddonRepository : Repository<InsuranceAddon>, IInsuranceAddonRepository
{
    public InsuranceAddonRepository(Context context) : base(context)
    {
    }

    public async Task<List<InsuranceAddon>> GetAllInsuranceAddons()
    {
        return await _context.InsuranceAddons.ToListAsync();
    }
}