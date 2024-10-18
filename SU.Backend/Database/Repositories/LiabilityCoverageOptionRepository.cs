﻿using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    /// <summary>
    /// This class is responsible for implementing the methods defined in the ILiabilityCoverageOptionRepository interface.
    /// </summary>
    public class LiabilityCoverageOptionRepository : Repository<LiabilityCoverageOptionRepository>, ILiabilityCoverageOptionRepository
    {
        public LiabilityCoverageOptionRepository(Context context) : base(context)
        {
        }

        public async Task<List<LiabilityCoverageOption>> GetLiabilityCoverageOptions()
        {
            return await _context.LiabilityCoverageOption.ToListAsync();
        }
    }
}
