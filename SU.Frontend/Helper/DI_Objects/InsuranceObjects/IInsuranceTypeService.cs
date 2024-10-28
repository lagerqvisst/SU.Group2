﻿using SU.Backend.Models.Enums.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Frontend.Helper.DI_Objects.InsuranceObjects
{
    public interface IInsuranceTypeService
    {
        InsuranceType InsuranceType { get; set; }
    }
}
