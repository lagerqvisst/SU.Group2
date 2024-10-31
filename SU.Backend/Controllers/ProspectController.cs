using Microsoft.Extensions.Logging;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Insurances.Prospects;
using SU.Backend.Services;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Controllers
{
    /// <summary>
    /// This class is responsible for handling the prospect controller.
    /// Makes logic available in the Viewmodel
    /// More info about the logic for each method can be found in the Service function each controller method uses.
    /// </summary>
    public class ProspectController
    {
        private readonly IProspectService _prospectService;
        private readonly IDataExportService _dataExportService;
        private readonly ILogger<ProspectController> _logger;

        public ProspectController(IProspectService prospectService, IDataExportService dataExportService, ILogger<ProspectController> logger)
        {
            _prospectService = prospectService;
            _dataExportService = dataExportService;

            _logger = logger;
        }

        public async Task<(List<Prospect>, string message)> IdentifyNewProspects()
        {
            _logger.LogInformation("Identifying new prospects...");
            var result = await _prospectService.IdentifyProspects();
            if (result.success)
            {
                _logger.LogInformation("Prospects identified successfully");
            }
            else
            {
                _logger.LogWarning($"Failed to identify prospects: {result.message}");
            }
            return (result.prospects, result.message);
        }

        public async Task<(bool success, string message)> AssignSellerToSpecificProspect(Employee employee, Prospect prospect)
        {
            _logger.LogInformation($"Assigning seller {employee.FirstName} {employee.LastName} to prospect {prospect.ProspectId}");
            var result = await _prospectService.AssignSellerToSpecificProspect(employee, prospect);
            if (result.success)
            {
                _logger.LogInformation($"Seller assigned to prospect successfully");
            }
            else
            {
                _logger.LogWarning($"Failed to assign seller to prospect: {result.message}");
            }
            return (result.success, result.message);
        }

        public async Task<(List<Prospect> prospects, string message)> GetAllCurrentProspects()
        {
            _logger.LogInformation("Getting all current prospects...");
            var result = await _prospectService.GetAllCurrentProspects();
            if (result.success)
            {
                _logger.LogInformation("Prospects retrieved successfully");
            }
            else
            {
                _logger.LogWarning($"Failed to retrieve prospects: {result.message}");
            }
            return (result.prospects, result.message);
        }

        public async Task<(bool success, string message)> UpdateProspect(Prospect prospect)
        {
            _logger.LogInformation("Prospect object updated via GUI");
            var result = await _prospectService.UpdateProspect(prospect);

            if (result.success)
            {
                _logger.LogInformation($"{result.message}");
                return (result.success, result.message);
            }
            else
            {
                _logger.LogWarning($"{result.message}");
                return (result.success, result.message);
            }
        }

        public async Task<(bool success, string message)> ExportProspectsToExcel(List<Prospect> prospects)
        {
            _logger.LogInformation("Exporting prospects to Excel...");

            var result = await _dataExportService.ExportProspects(prospects);

            if (result.success)
            {
                _logger.LogInformation("Prospects exported successfully");
                return (result.success, result.message);
            }
            else
            {
                _logger.LogWarning($"Failed to export prospects: {result.message}");
                return (result.success, result.message);
            }



        }
    }
}

