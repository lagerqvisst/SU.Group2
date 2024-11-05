using Microsoft.Extensions.Logging;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Statistics;
using SU.Backend.Services.Interfaces;

namespace SU.Backend.Controllers;

/// <summary>
///     This class is responsible for handling the statistics controller.
///     Makes logic available in the Viewmodel
///     More info about the logic for each method can be found in the Service function each controller method uses.
/// </summary>
public class StatisticsController
{
    private readonly IDataExportService _dataExportService;

    private readonly ILogger<StatisticsController> _logger;

    // Services
    private readonly IStatisticsService _statisticsService;

    // Constructor
    public StatisticsController(IStatisticsService statisticsService, ILogger<StatisticsController> logger,
        IDataExportService dataExportService)
    {
        _statisticsService = statisticsService;
        _logger = logger;
        _dataExportService = dataExportService;
    }


    // Controller for GetMonthlyInsuranceStats method
    public async Task<(List<InsuranceStatistics>, string message)> GetMonthlyInsuranceStats()
    {
        _logger.LogInformation("Getting monthly insurance statistics...");

        var result = await _statisticsService.GetMonthlyInsuranceStatistics();

        if (result.success)
            _logger.LogInformation("Monthly insurance statistics retrieved successfully");
        else
            _logger.LogWarning("Error retrieving monthly insurance statistics: {result.Message}");
        return (result.statistics, result.message);
    }

    public async Task<(bool success, string message, SellerStatistics statistics)> SellerStatisticsBySeller(int year,
        Employee seller)
    {
        _logger.LogInformation("Getting seller statistics for year {year} and seller {seller}", year, seller);

        var result = await _statisticsService.GetSellerStatisticsBySeller(year, seller);

        if (result.success)
        {
            _logger.LogInformation("Seller statistics retrieved successfully");
            return (result.success, result.message, result.statistics);
        }

        _logger.LogWarning("Error retrieving seller statistics: {result.Message}");
        return (result.success, result.message, null);
    }

    public async Task<(bool success, string message, List<SellerStatistics> statistics)> GetActiveSellerStatistics(
        int year, List<InsuranceType>? insuranceTypes = null)

    {
        _logger.LogInformation("Getting active seller statistics for year {year}", year);

        var result = await _statisticsService.GetActiveSellerStatistics(year, insuranceTypes);

        if (result.success)
        {
            _logger.LogInformation("Active seller statistics retrieved successfully");
            return (result.success, result.message, result.statistics);
        }

        _logger.LogWarning("Error retrieving active seller statistics: {result.Message}");
        return (result.success, result.message, null);
    }

    public async Task<(bool success, string message, List<SellerStatistics> statistics)> GetSellerStatistics(int year,
        List<InsuranceType>? insuranceTypes = null)
    {
        _logger.LogInformation("Getting seller statistics for year {year}", year);

        var result = await _statisticsService.GetSellerStatistics(year, insuranceTypes);

        if (result.success)
        {
            _logger.LogInformation("Seller statistics retrieved successfully");
            return (result.success, result.message, result.statistics);
        }

        _logger.LogWarning("Error retrieving seller statistics: {result.Message}");
        return (result.success, result.message, null);
    }

    public async Task<(bool success, string message)> ExportTable(List<SellerStatistics> statistics,
        bool isPrivateInsurance)
    {
        _logger.LogInformation("Exporting table...");

        var result = await _dataExportService.ExportSellerStatisticsToExcel(statistics, isPrivateInsurance);

        if (result.success)
        {
            _logger.LogInformation("Table exported successfully");
            return (result.success, result.message);
        }

        _logger.LogWarning($"Failed to export table: {result.message}");
        return (result.success, result.message);
    }

    public async Task<(bool success, string message)> ExportBarChart(SellerStatistics statistics)
    {
        _logger.LogInformation("Exporting bar chart...");

        var result = await _dataExportService.ExportBarChartStatisticsToExcel(statistics);

        if (result.success)
        {
            _logger.LogInformation("Bar chart exported successfully");
            return (result.success, result.message);
        }

        _logger.LogWarning($"Failed to export bar chart: {result.message}");
        return (result.success, result.message);
    }

    public async Task<(bool success, string message)> ExportLineChart(List<SellerStatistics> statistics)
    {
        _logger.LogInformation("Exporting line chart...");

        var result = await _dataExportService.ExportLineChartStatisticsToExcel(statistics);

        if (result.success)
        {
            _logger.LogInformation("Line chart exported successfully");
            return (result.success, result.message);
        }

        _logger.LogWarning($"Failed to export line chart: {result.message}");
        return (result.success, result.message);
    }
}