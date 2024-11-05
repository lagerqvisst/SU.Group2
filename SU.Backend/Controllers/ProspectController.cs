using Microsoft.Extensions.Logging;
using SU.Backend.Models.Insurances.Prospects;
using SU.Backend.Services.Interfaces;

namespace SU.Backend.Controllers;

/// <summary>
///     This class is responsible for handling the prospect controller.
///     Makes logic available in the Viewmodel
///     More info about the logic for each method can be found in the Service function each controller method uses.
/// </summary>
public class ProspectController
{
    private readonly IDataExportService _dataExportService;

    private readonly ILogger<ProspectController> _logger;

    // Services
    private readonly IProspectService _prospectService;

    // Constructor
    public ProspectController(IProspectService prospectService, IDataExportService dataExportService,
        ILogger<ProspectController> logger)
    {
        _prospectService = prospectService;
        _dataExportService = dataExportService;

        _logger = logger;
    }

    // Controller for IdentifyNewProspects method
    public async Task<(List<Prospect> prospects, string message)> IdentifyNewProspects()
    {
        _logger.LogInformation("Identifying new prospects...");
        var result = await _prospectService.GenerateProspectData();
        if (result.success)
            _logger.LogInformation("Prospects identified successfully");
        else
            _logger.LogWarning($"Failed to identify prospects: {result.message}");
        return (result.prospects, result.message);
    }


    // Controller for ExportProspectsToExcel method
    public async Task<(bool success, string message)> ExportProspectsToExcel(List<Prospect> prospects)
    {
        _logger.LogInformation("Exporting prospects to Excel...");

        var result = await _dataExportService.ExportProspects(prospects);

        if (result.success)
        {
            _logger.LogInformation("Prospects exported successfully");
            return (result.success, result.message);
        }

        _logger.LogWarning($"Failed to export prospects: {result.message}");
        return (result.success, result.message);
    }
}