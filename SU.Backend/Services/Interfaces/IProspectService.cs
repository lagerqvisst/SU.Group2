using SU.Backend.Models.Insurance.Prospects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    public interface IProspectService
    {
        Task<(bool Success, string Message, List<Prospect> prospects)> IdentifyProspects();

        Task<(bool Success, string Message)> TestAssignSellerToProspect();
    }
}
