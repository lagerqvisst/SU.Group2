using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Statistics;

namespace SU.Backend.Services.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the StatisticsService class must implement.
/// </summary>
public interface IStatisticsService
{
    Task<(bool success, string message, List<SellerStatistics> statistics)> GetSellerStatistics(int year,
        List<InsuranceType>? insuranceTypes = null);

    Task<(bool success, string message, List<SellerStatistics> statistics)> GetActiveSellerStatistics(int year,
        List<InsuranceType>? insuranceTypes = null);

    Task<(bool success, string message, List<InsuranceStatistics> statistics)> GetMonthlyInsuranceStatistics();

    Task<(bool success, string message, SellerStatistics statistics)> GetSellerStatisticsBySeller(int year,
        Employee seller);
}