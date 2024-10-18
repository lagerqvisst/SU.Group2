﻿using Microsoft.Extensions.Logging;
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
    /// <summary>
    /// This class is responsible for handling the private coverage options and private coverages.
    /// Makes the logic available in the Viewmodel
    /// More info about the logic for each method can be found in the Service function each controller method uses.
    /// </summary>
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

        public async Task<(List<PrivateCoverageOption>, string Message)> GetAllPrivateCoverageOptions()
        {
            _logger.LogInformation("Controller activated to get all private coverage options...");
            var result = await _privateCoverageService.GetAllPrivateCoverageOptions();

            if (result.Success)
            {
                _logger.LogInformation($"Private coverage options found:\n{result.Message}");
                return (result.PrivateCoverageOptions, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error getting private coverage options: {result.Message}");
                return (new List<PrivateCoverageOption>(), result.Message);
            }
        }

        public async Task<(List<PrivateCoverage>, string Message)> GetAllPrivateCoverages()
        {
            _logger.LogInformation("Controller activated to get all private coverages...");
            var result = await _privateCoverageService.GetAllPrivateCoverages();

            if (result.Success)
            {
                _logger.LogInformation($"Private coverages found:\n{result.Message}");
                return (result.PrivateCoverages, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error getting private coverages: {result.Message}");
                return (new List<PrivateCoverage>(), result.Message);
            }
        }
    }
}
