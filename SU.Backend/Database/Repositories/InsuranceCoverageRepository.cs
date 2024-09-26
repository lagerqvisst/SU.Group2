﻿using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public class InsuranceCoverageRepository : Repository<InsuranceCoverage>
    {
        public InsuranceCoverageRepository(Context context) : base(context)
        {
        }
    }
}