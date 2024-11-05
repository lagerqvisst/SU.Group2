using SU.Backend.Models.Insurances;

namespace SU.Backend.Database.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the InsuranceAddonRepository class must implement.
/// </summary>
public interface IInsuranceAddonRepository
{
    Task<List<InsuranceAddon>> GetAllInsuranceAddons();
}