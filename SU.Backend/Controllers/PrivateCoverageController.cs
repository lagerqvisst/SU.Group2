using Microsoft.Extensions.Logging;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances.Coverage;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Controllers
{
    public class PrivateCoverageController
    {
        private readonly IPrivateCoverageService _privateCoverageService;
        private readonly ILogger<PrivateCoverageController> _logger;

        public PrivateCoverageController(IPrivateCoverageService privateCoverageService, ILogger<PrivateCoverageController> logger)
        {
            _privateCoverageService = privateCoverageService;
            _logger = logger;
        }

        public async Task<(PrivateCoverageOption?, string Message)> GetPrivateCoverageOption(decimal coverageAmount, InsuranceType insuranceType)
        {
            _logger.LogInformation("Controller activated to get private coverage option...");
            var result = await _privateCoverageService.GetPrivateCoverageOptionAsync(coverageAmount, insuranceType);

            if (result.Success)
            {
                _logger.LogInformation($"Private coverage option found:\n{result.Message}");
                return (result.CoverageOption, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error getting private coverage option: {result.Message}");
                return (null, result.Message);
            }
        }

        public async Task<(List<PrivateCoverageOption>, string Message)> GetAllOptions()
        {
            _logger.LogInformation("Controller activated to get all private coverage options...");
            var result = await _privateCoverageService.GetAllPrivateCoverageOption();

            if (result.Success)
            {
                _logger.LogInformation($"Private coverage options found:\n{result.Message}");
                return (result.Opions, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error getting private coverage options: {result.Message}");
                return (new List<PrivateCoverageOption>(), result.Message);
            }
        }
    }
}
