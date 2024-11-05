using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Database.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the PrivateCoveragesRepository class must implement.
/// </summary>
public interface IPrivateCoveragesRepository
{
    Task<List<PrivateCoverage>> GetAllPrivateCoverages();
}