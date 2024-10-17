using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    public interface IPrivateCoverageService
    {
        Task<(bool Success, PrivateCoverageOption? CoverageOption, string Message)> GetPrivateCoverageOptionAsync(decimal coverageAmount, InsuranceType insuranceType);
        Task<(bool Success, string Message, List<PrivateCoverageOption> PrivateCoverageOptions)> GetAllPrivateCoverageOptions(); 
    }
}
