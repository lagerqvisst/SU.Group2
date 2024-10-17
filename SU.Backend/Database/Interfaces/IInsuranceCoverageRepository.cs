using SU.Backend.Models.Comissions;
using SU.Backend.Models.Insurances;
using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Interfaces
{
    public interface IInsuranceCoverageRepository
    {
        Task<List<InsuranceCoverage>> GetAllInsuranceCoverages();
    }
}
