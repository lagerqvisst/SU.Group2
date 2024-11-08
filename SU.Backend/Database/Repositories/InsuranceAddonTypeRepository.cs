using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Enums.Insurance.Addons;
using SU.Backend.Models.Insurances;

namespace SU.Backend.Database.Repositories;

/// <summary>
///     This class is responsible for implementing the methods defined in the IInsuranceAddonTypeRepository interface.
/// </summary>
public class InsuranceAddonTypeRepository : Repository<InsuranceAddonType>, IInsuranceAddonTypeRepository
{
    public InsuranceAddonTypeRepository(Context context) : base(context)
    {
    }
    //This method is used to get all insurance addon types
    public async Task<List<InsuranceAddonType>> GetAllInsuranceAddonTypes()
    {
        return _context.InsuranceAddonTypes.ToList();
    }

    //This method is used to get a specific addon type based on the addon type and coverage amount
    public async Task<InsuranceAddonType> GetSpecificAddonType(AddonType addonType, decimal coverageAmount)
    {
        return await _context.InsuranceAddonTypes
            .FirstOrDefaultAsync(x => x.Description == addonType && x.CoverageAmount == coverageAmount);
    }
}