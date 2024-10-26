using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    /// <summary>
    /// This interface is responsible for defining the methods that the PrivateCoverageService class must implement.
    /// </summary>
    public interface IPrivateCoverageService
    {
        Task<(bool success, PrivateCoverageOption? coverageOption, string message)> GetPrivateCoverageOptionAsync(decimal coverageAmount, InsuranceType insuranceType);
        Task<(bool success, string message, List<PrivateCoverageOption> privateCoverageOptions)> GetAllPrivateCoverageOptions(); 
        Task<(bool success, string message, List<PrivateCoverage> privateCoverages)> GetAllPrivateCoverages();
    }
}
