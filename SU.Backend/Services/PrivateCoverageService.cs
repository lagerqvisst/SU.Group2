using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances.Coverage;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    public class PrivateCoverageService : IPrivateCoverageService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<PrivateCoverageService> _logger;

        // Injicera UnitOfWork och Logger i konstruktorn
        public PrivateCoverageService(UnitOfWork unitOfWork, ILogger<PrivateCoverageService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<(bool Success, string Message, List<PrivateCoverageOption> Options)> GetAllPrivateCoverageOption()
        {
            _logger.LogInformation("Controller activated to get all private coverage options...");

            try
            {
                var coverageOptions = _unitOfWork.PrivateCoverageOptions.GetPrivateCoverageOptions();
                _logger.LogInformation("Private coverage options found: {CoverageOptionsCount}", coverageOptions.Result.Count);

                return (true, "Private coverage options found.", coverageOptions.Result);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error occurred while fetching private coverage options.");
                return (false, "An error occurred while fetching the private coverage options.", new List<PrivateCoverageOption>());
            }
        }

        // Metod för att hämta en specifik PrivateCoverageOption
        public async Task<(bool Success, PrivateCoverageOption? CoverageOption, string Message)> GetPrivateCoverageOptionAsync(decimal coverageAmount, InsuranceType insuranceType)
        {
            try
            {
                var currentYear = DateTime.Now.Year;
                var coverageOption = await _unitOfWork.PrivateCoverageOptions
                    .GetSpecificPrivateCoverageOption(coverageAmount, new DateTime(currentYear, 1, 1), insuranceType);

                if (coverageOption == null)
                {
                    _logger.LogWarning("No private coverage option found for amount: {CoverageAmount} and insurance type: {InsuranceType}", coverageAmount, insuranceType);
                    return (false, null, "No private coverage option found for the input.");
                }

                _logger.LogInformation("Private coverage option found: {CoverageOptionId}", coverageOption.PrivateCoverageOptionId);
                return (true, coverageOption, "Private coverage option found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching private coverage option.");
                return (false, null, "An error occurred while fetching the private coverage option.");
            }
        }
    }
}

