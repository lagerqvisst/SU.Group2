﻿using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Database.Repositories;

/// <summary>
///     This class is responsible for implementing the methods defined in the ILiabilityCoverageRepository interface.
/// </summary>
public class LiabilityCoverageRepository : Repository<LiabilityCoverage>, ILiabilityCoverageRepository
{
    public LiabilityCoverageRepository(Context context) : base(context)
    {
    }

    //This method is used to get all liability coverages
    public async Task<List<LiabilityCoverage>> GetLiabilityCoverage()
    {
        return await _context.LiabilityCoverages.ToListAsync();
    }
}