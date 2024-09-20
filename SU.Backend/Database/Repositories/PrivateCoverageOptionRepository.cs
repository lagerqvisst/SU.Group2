﻿using SU.Backend.Models.Insurance.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public class PrivateCoverageOptionRepository : Repository<PrivateCoverageOption>, IPrivateCoverageOptionRepository
    {
        public PrivateCoverageOptionRepository(Context context) : base(context)
        {
        }
    }
}