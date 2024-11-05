using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Database.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the RiskzoneRepository class must implement.
/// </summary>
public interface IRiskzoneRepository
{
    Task<List<RiskZone>> GetAllRiskZones();
}