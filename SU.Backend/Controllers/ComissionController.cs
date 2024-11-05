using Microsoft.Extensions.Logging;
using SU.Backend.Models.Comissions;
using SU.Backend.Services.Interfaces;

namespace SU.Backend.Controllers;

/// <summary>
///     This class is responsible for handling the business logic of the commissions.
///     Makes logic available in the frontend.
///     More info about the logic for each method can be found in the Service function each controller method uses.
/// </summary>
public class ComissionController
{
    // Services
    private readonly ICommissionService _commissionService;
    private readonly IDataExportService _dataExportService;
    private readonly ILogger<ComissionController> _logger;

    // Constructor
    public ComissionController(ICommissionService commissionService, IDataExportService dataExportService,
        ILogger<ComissionController> logger)
    {
        _commissionService = commissionService;
        _dataExportService = dataExportService;
        _logger = logger;
    }

    // Controller for GetCommissions method
    public async Task<(string message, List<Commission>)> GetCommissions(DateTime startDate, DateTime endDate)
    {
        _logger.LogInformation("Getting commissions for year {year}");

        var result = await _commissionService.GetAllCommissions(startDate, endDate);

        if (result.success)
            _logger.LogInformation("Commissions retrieved successfully");
        else
            _logger.LogWarning("Error retrieving commissions: {result.Message}");
        return (result.message, result.commissions);
    }

    // Controller for ExportCommissionsToExcel method
    public async Task<(bool success, string message)> ExportCommissionsToExcel(List<Commission> commissions)
    {
        _logger.LogInformation("Exporting commissions to Excel...");

        var result = await _dataExportService.ExportCommissionsToExcel(commissions);

        if (result.success)
            _logger.LogInformation("Commissions exported successfully");
        else
            _logger.LogWarning("Error exporting commissions: {result.Message}");

        return (result.success, result.message);
    }
}