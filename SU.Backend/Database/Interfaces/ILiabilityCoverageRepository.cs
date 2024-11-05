using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Database.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the LiabilityCoverageRepository class must implement.
/// </summary>
public interface ILiabilityCoverageRepository
{
    Task<List<LiabilityCoverage>> GetLiabilityCoverage();
}