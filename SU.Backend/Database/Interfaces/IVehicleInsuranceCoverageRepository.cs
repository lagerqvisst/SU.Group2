using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Database.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the VehicleInsuranceCoverageRepository class must
///     implement.
/// </summary>
public interface IVehicleInsuranceCoverageRepository
{
    Task<List<VehicleInsuranceCoverage>> GetAllVehicleInsuranceCoverages();
}