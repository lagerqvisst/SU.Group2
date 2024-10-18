﻿using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Enums.Insurance.Addons;
using SU.Backend.Models.Insurances;
using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    /// <summary>
    /// This class is responsible for implementing the methods defined in the IInsuranceAddonTypeRepository interface.
    /// </summary>
    public class InsuranceAddonTypeRepository : Repository<InsuranceAddonType>, IInsuranceAddonTypeRepository
    {
        public InsuranceAddonTypeRepository(Context context) : base(context)
        {
        }

        public async Task <List<InsuranceAddonType>> GetAllInsuranceAddonTypes()
        {
            return await _context.InsuranceAddonTypes.ToListAsync();
        }

        //This method is used to get a specific addon type based on the addon type and coverage amount
        public async Task<InsuranceAddonType> GetSpecificAddonType(AddonType addonType, decimal coverageAmount)
        {
            return await _context.InsuranceAddonTypes
                .FirstOrDefaultAsync(x => x.Description == addonType && x.CoverageAmount == coverageAmount);
        }
    }
}
