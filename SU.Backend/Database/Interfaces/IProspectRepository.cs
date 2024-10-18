using SU.Backend.Models.Insurances.Prospects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Interfaces
{
    /// <summary>
    /// This interface is responsible for defining the methods that the ProspectRepository class must implement.
    /// </summary>
    public interface IProspectRepository
    {
        Task<List<Prospect>> GetAllProspects();
        Task<bool> ProspectExists(int? privateCustomerId, int? companyCustomerId); // Kolla om en prospect finns

    }
}
