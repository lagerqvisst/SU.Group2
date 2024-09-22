using SU.Backend.Models.Insurance.Prospects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Interfaces
{
    public interface IProspectRepository
    {
        Task<List<Prospect>> GetAllProspects();
        Task<bool> ProspectExists(int? privateCustomerId, int? companyCustomerId); // Kolla om en prospect finns

    }
}
