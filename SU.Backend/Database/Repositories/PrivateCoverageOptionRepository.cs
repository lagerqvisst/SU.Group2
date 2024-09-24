using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public class PrivateCoverageOptionRepository : Repository<PrivateCoverageOption>, IPrivateCoverageOptionRepository
    {
        public PrivateCoverageOptionRepository(Context context) : base(context)
        {
        }

        public async Task<List<PrivateCoverageOption>> GetPrivateCoverageOptions()
        {
            return _context.PrivateCoverageOption.ToList();
        }

        public async Task<PrivateCoverageOption> GetSpecificPrivateCoverageOption(decimal coverageAmount, DateTime startDate, InsuranceType insuranceType)
        {
            int year = startDate.Year; // Få året från startdatumet

            return await _context.PrivateCoverageOption
                .FirstOrDefaultAsync(x => x.CoverageAmount == coverageAmount && x.StartDate.Year == year && x.InsuranceType == insuranceType);
        }


    }
}
