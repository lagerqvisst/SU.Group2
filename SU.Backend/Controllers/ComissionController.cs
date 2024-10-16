using Microsoft.Extensions.Logging;
using SU.Backend.Models.Comissions;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Controllers
{
    public class ComissionController
    {
        ICommissionService _commissionService;
        ILogger<ComissionController> _logger;

        public ComissionController(ICommissionService commissionService, ILogger<ComissionController> logger)
        {
            _commissionService = commissionService;
            _logger = logger;
        }

        public async Task<(string Message, List<Commission>)> GetCommissions(DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation("Getting commissions for year {year}");

            var result = await _commissionService.GetAllCommissions(startDate, endDate);

            if (result.Success)
            {
                _logger.LogInformation("Commissions retrieved successfully");
            }
            else
            {
                _logger.LogWarning("Error retrieving commissions: {result.Message}");
            }
            return (result.Message, result.Commissions);
        }
    }
}
