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
        // Services
        IStatisticsService _statisticsService;
        ILogger<StatisticsController> _logger;

        // Constructor
        public StatisticsController(IStatisticsService statisticsService, ILogger<StatisticsController> logger)
        {
            _statisticsService = statisticsService;
            _logger = logger;
        }

        // Controller for GetSellerStatistics method
        public async Task<(string message, List<SellerStatistics> statistics)> GetSellerStatistics(int year, List<InsuranceType>? insuranceTypes = null)
        {
            _logger.LogInformation("Getting seller statistics for year {year}", year);

            var result = await _statisticsService.GetSellerStatistics(year, insuranceTypes);

            // Check if the result is successful
            if (result.statistics == null || !result.statistics.Any())
            {
                _logger.LogWarning("No statistic found for chosen seller");
                return (result.message, null); // Return null if no statistics were found
            }

            _logger.LogInformation("Seller statistics retrieved successfully");

            // Only returned if the result is successful
            return (result.message, result.statistics);
        }

        // Controller for GetMonthlyInsuranceStats method
        public async Task<(List<InsuranceStatistics>, string message)> GetMonthlyInsuranceStats()
        {
            _logger.LogInformation("Getting monthly insurance statistics...");

            var result = await _statisticsService.GetMonthlyInsuranceStatistics();

            if (result.success)
            {
                _logger.LogInformation("Monthly insurance statistics retrieved successfully");
            }
            else
            {
                _logger.LogWarning("Error retrieving monthly insurance statistics: {result.Message}");
            }
            return (result.statistics, result.message);
        }
    }
}