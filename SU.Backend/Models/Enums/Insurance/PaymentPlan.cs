using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Enums.Insurance
{
    /// <summary>
    ///  Taken from business documentation.
    /// </summary>
    public enum PaymentPlan
    {
        Annual,   // Helår
        SemiAnnual,  // Halvår
        Quarterly, // Kvartal
        Monthly   // Månad
    }
}
