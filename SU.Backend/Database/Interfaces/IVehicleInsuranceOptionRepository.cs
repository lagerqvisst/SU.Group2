using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Database.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the VehicleInsuranceOptionRepository class must
///     implement.
/// </summary>
public interface IVehicleInsuranceOptionRepository
{
    Task<List<VehicleInsuranceOption>> GetVehicleInsuranceOptions();
}