using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Database.Repositories;

/// <summary>
///     This class is responsible for implementing the methods defined in the IInsuranceCoverageRepository interface.
/// </summary>
public class InsuranceCoverageRepository : Repository<InsuranceCoverage>, IInsuranceCoverageRepository
{
    public InsuranceCoverageRepository(Context context) : base(context)
    {
    }

    //This method is used to get all insurance coverages
    public async Task<List<InsuranceCoverage>> GetAllInsuranceCoverages()
    {
        return await _context.InsuranceCoverages.ToListAsync();
    }
}