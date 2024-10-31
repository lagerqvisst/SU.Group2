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
        private readonly IProspectService prospectService;
        private readonly ILogger<ProspectController> _logger;

        public ProspectController(IProspectService prospectService, ILogger<ProspectController> logger)
        {
            this.prospectService = prospectService;
            _logger = logger;
        }

        public async Task<(List<Prospect>, string message)> IdentifyNewProspects()
        {
            _logger.LogInformation("Identifying new prospects...");
            var result = await prospectService.IdentifyProspects();
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
            var result = await prospectService.AssignSellerToSpecificProspect(employee, prospect);
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
            var result = await prospectService.GetAllCurrentProspects();
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
            var result = await prospectService.UpdateProspect(prospect);

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
    }
}

