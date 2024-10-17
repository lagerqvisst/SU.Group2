using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Interfaces
{
    public interface IPrivateCoverageOptionRepository
    {
        Task<List<PrivateCoverageOption>> GetAllPrivateCoverageOptions();

        Task<PrivateCoverageOption> GetSpecificPrivateCoverageOption(decimal coverageAmount, DateTime startDate, InsuranceType insuranceType); 
    }
}
