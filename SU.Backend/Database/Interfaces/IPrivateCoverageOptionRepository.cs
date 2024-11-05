using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Database.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the PrivateCoverageOptionRepository class must
///     implement.
/// </summary>
public interface IPrivateCoverageOptionRepository
{
    Task<List<PrivateCoverageOption>> GetAllPrivateCoverageOptions();

    Task<PrivateCoverageOption> GetSpecificPrivateCoverageOption(decimal coverageAmount, DateTime startDate,
        InsuranceType insuranceType);

    Task<List<PrivateCoverageOption>> GetSpecificCoverageInCurrentYear(InsuranceType insurance);
}