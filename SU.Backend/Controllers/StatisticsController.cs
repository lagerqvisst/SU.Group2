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
    /// <summary>
    /// This class is responsible for handling the statistics controller.
    /// Makes logic available in the Viewmodel
    /// More info about the logic for each method can be found in the Service function each controller method uses.
    /// </summary>
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

        public async Task<(List<InsuranceStatistics>, string Message)> GetMonthlyInsuranceStats()
        {
            _logger.LogInformation("Getting monthly insurance statistics...");

            var result = await _statisticsService.GetMonthlyInsuranceStatistics();

            if (result.Success)
            {
                _logger.LogInformation("Monthly insurance statistics retrieved successfully");
            }
            else
            {
                _logger.LogWarning("Error retrieving monthly insurance statistics: {result.Message}");
            }
            return (result.Statistics, result.Message);
        }
    }
}
