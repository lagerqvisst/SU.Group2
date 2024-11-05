using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Database.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the InsuranceCoverageRepository class must implement.
/// </summary>
public interface IInsuranceCoverageRepository
{
    Task<List<InsuranceCoverage>> GetAllInsuranceCoverages();
}