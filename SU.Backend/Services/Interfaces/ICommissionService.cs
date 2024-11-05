using SU.Backend.Models.Comissions;

namespace SU.Backend.Services.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the CommissionService class must implement.
/// </summary>
public interface ICommissionService
{
    Task<(bool success, string message, List<Commission> commissions)> GetAllCommissions(DateTime startDate,
        DateTime endDate);
}