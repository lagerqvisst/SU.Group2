using Microsoft.Extensions.Logging;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Insurances.Prospects;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Controllers
{
    public class ProspectController
    {
        private readonly IProspectService prospectService;
        private readonly ILogger<ProspectController> _logger;

        public ProspectController(IProspectService prospectService, ILogger<ProspectController> logger)
        {
            this.prospectService = prospectService;
            _logger = logger;
        }

        public async Task<(List<Prospect>, string Message)> IdentifyNewProspects()
        {
            _logger.LogInformation("Identifying new prospects...");
            var result = await prospectService.IdentifyProspects();
            if (result.Success)
            {
                _logger.LogInformation("Prospects identified successfully");
            }
            else
            {
                _logger.LogWarning($"Failed to identify prospects: {result.Message}");
            }
            return (result.prospects, result.Message);
        }

        public async Task<(bool Success, string Message)> AssignSellerToSpecificProspect(Employee employee, Prospect prospect)
        {
            _logger.LogInformation($"Assigning seller {employee.FirstName} {employee.LastName} to prospect {prospect.ProspectId}");
            var result = await prospectService.AssignSellerToSpecificProspect(employee, prospect);
            if (result.Success)
            {
                _logger.LogInformation($"Seller assigned to prospect successfully");
            }
            else
            {
                _logger.LogWarning($"Failed to assign seller to prospect: {result.Message}");
            }
            return (result.Success, result.Message);
        }

        public async Task<(List<Prospect>, string Message)> GetAllCurrentProspects()
        {
            _logger.LogInformation("Getting all current prospects...");
            var result = await prospectService.GetAllCurrentProspects();
            if (result.Success)
            {
                _logger.LogInformation("Prospects retrieved successfully");
            }
            else
            {
                _logger.LogWarning($"Failed to retrieve prospects: {result.Message}");
            }
            return (result.prospects, result.Message);
        }
    }
}

