using SU.Backend.Models.Employees;
using SU.Backend.Models.Insurances.Prospects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    /// <summary>
    /// This interface is responsible for defining the methods that the ProspectService class must implement.
    /// </summary>
    public interface IProspectService
    {
        Task<(bool Success, string Message, List<Prospect> prospects)> IdentifyProspects();

        Task<(bool Success, string Message)> AssignSellerToSpecificProspect(Employee employee, Prospect prospect);

        Task<(bool Success, string Message, List<Prospect> prospects)> GetAllCurrentProspects();
    }
}
