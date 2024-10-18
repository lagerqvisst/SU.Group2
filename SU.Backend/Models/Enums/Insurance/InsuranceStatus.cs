﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Enums.Insurance
{
    public enum InsuranceStatus
    {
        Requested, // Represents "Förfrågan".
        Active,    // Represents "Aktiv / Signerad"
        Terminated // Represents "Avslutad"
    }
}

