using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Enums.Insurance.Addons;
using SU.Backend.Models.Insurances;
using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Interfaces
{
    public interface IInsuranceAddonTypeRepository
    {
        Task<List<InsuranceAddonType>> ListAllInsuranceAddonTypes();

        Task<InsuranceAddonType> GetSpecificAddonType(AddonType addonType, decimal coverageAmount);
    }
}
