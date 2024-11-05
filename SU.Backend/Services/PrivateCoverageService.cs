using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances.Coverage;
using SU.Backend.Services.Interfaces;

namespace SU.Backend.Services;

/// <summary>
///     This class is responsible for handling the business logic for private coverages.
/// </summary>
public class PrivateCoverageService : IPrivateCoverageService
{
    private readonly ILogger<PrivateCoverageService> _logger;
    private readonly UnitOfWork _unitOfWork;

    public PrivateCoverageService(UnitOfWork unitOfWork, ILogger<PrivateCoverageService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    // Method to get all private coverage options
    public async Task<(bool success, string message, List<PrivateCoverageOption> privateCoverageOptions)>
        GetAllPrivateCoverageOptions()
    {
        _logger.LogInformation("Controller activated to get all private coverage options...");

        try
        {
            var coverageOptions = _unitOfWork.PrivateCoverageOptions.GetAllPrivateCoverageOptions();
            _logger.LogInformation("Private coverage options found: {CoverageOptionsCount}",
                coverageOptions.Result.Count);

            return (true, "Private coverage options found.", coverageOptions.Result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching private coverage options.");
            return (false, "An error occurred while fetching the private coverage options.",
                new List<PrivateCoverageOption>());
        }
    }

    // Method to get all private coverages
    public Task<(bool success, string message, List<PrivateCoverage> privateCoverages)> GetAllPrivateCoverages()
    {
        _logger.LogInformation("Controller activated to get all private coverages...");

        try
        {
            var privatecoverages = _unitOfWork.PrivateCoverages.GetAllPrivateCoverages();
            _logger.LogInformation("Private coverages found: {CoveragesCount}", privatecoverages.Result.Count);

            return Task.FromResult((true, "Private coverages found.", privatecoverages.Result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching private coverages.");
            return Task.FromResult((false, "An error occurred while fetching the private coverages.",
                new List<PrivateCoverage>()));
        }
    }

    // Method to get a specific private coverage option
    public async Task<(bool success, PrivateCoverageOption? coverageOption, string message)>
        GetPrivateCoverageOptionAsync(decimal coverageAmount, InsuranceType insuranceType)
    {
        try
        {
            var currentYear = DateTime.Now.Year;
            var coverageOption = await _unitOfWork.PrivateCoverageOptions
                .GetSpecificPrivateCoverageOption(coverageAmount, new DateTime(currentYear, 1, 1), insuranceType);

            if (coverageOption == null)
            {
                _logger.LogWarning(
                    "No private coverage option found for amount: {CoverageAmount} and insurance type: {InsuranceType}",
                    coverageAmount, insuranceType);
                return (false, null, "No private coverage option found for the input.");
            }

            _logger.LogInformation("Private coverage option found: {CoverageOptionId}",
                coverageOption.PrivateCoverageOptionId);
            return (true, coverageOption, "Private coverage option found.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching private coverage option.");
            return (false, null, "An error occurred while fetching the private coverage option.");
        }
    }
}