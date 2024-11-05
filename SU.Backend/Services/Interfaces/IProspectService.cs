using SU.Backend.Models.Insurances.Prospects;

namespace SU.Backend.Services.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the ProspectService class must implement.
/// </summary>
public interface IProspectService
{
    Task<(bool success, string message, List<Prospect> prospects)> GenerateProspectData();
}