﻿using SU.Backend.Models.Insurance.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Interfaces
{
    public interface IPrivateCoverageOptionRepository
    {
        Task<List<PrivateCoverageOption>> GetPrivateCoverageOptions();
    }
}
