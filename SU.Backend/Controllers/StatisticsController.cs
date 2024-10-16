using Microsoft.Extensions.Logging;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Statistics;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Controllers
{
    public class StatisticsController
    {
        IStatisticsService _statisticsService;
        ILogger<StatisticsController> _logger;

        public StatisticsController(IStatisticsService statisticsService, ILogger<StatisticsController> logger)
        {
            _statisticsService = statisticsService;
            _logger = logger;
        }

        public async Task<(string Message, List<SellerStatistics>)> GetSellerStatistics(int year, List<InsuranceType>? insuranceTypes = null)
        {
            _logger.LogInformation("Getting seller statistics for year {year}", year);

            var result = await _statisticsService.GetSellerStatistics(year, insuranceTypes);

            if (result.Success)
            {
                _logger.LogInformation("Seller statistics retrieved successfully");
            }
            else
            {
                _logger.LogWarning("Error retrieving seller statistics: {result.Message}");
            }
            return (result.Message, result.Statistics);
        }
    }
}
