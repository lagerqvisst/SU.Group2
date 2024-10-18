using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Enums.Prospects
{
    /// <summary>
    /// Represents the outcome of seller acting on a prospect.
    /// </summary>
    public enum ProspectStatus
    {
        NotContacted,
        Contacted,
        Rejected,
        Accepted
    }
}
