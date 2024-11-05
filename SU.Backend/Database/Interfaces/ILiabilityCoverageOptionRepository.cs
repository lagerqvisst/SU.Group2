using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Database.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the LiabilityCoverageOptionRepository class must
///     implement.
/// </summary>
public interface ILiabilityCoverageOptionRepository
{
    Task<List<LiabilityCoverageOption>> GetLiabilityCoverageOptions();
}