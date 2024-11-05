using SU.Backend.Models.Enums.Insurance.Addons;
using SU.Backend.Models.Insurances;

namespace SU.Backend.Database.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the InsuranceAddonTypeRepository class must implement.
/// </summary>
public interface IInsuranceAddonTypeRepository
{
    Task<List<InsuranceAddonType>> GetAllInsuranceAddonTypes();

    Task<InsuranceAddonType> GetSpecificAddonType(AddonType addonType, decimal coverageAmount);
}