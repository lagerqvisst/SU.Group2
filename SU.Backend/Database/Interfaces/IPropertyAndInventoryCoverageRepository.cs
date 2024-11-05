using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Database.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the PropertyAndInventoryCoverageRepository class must
///     implement.
/// </summary>
public interface IPropertyAndInventoryCoverageRepository
{
    Task<List<PropertyAndInventoryCoverage>> GetAllPropertyAndInventoryCoverages();
}