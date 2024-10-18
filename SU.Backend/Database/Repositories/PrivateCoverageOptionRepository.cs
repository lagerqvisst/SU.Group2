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
    /// <summary>
    /// This class is responsible for implementing the methods defined in the IPrivateCoverageOptionRepository interface.
    /// </summary>
    public class PrivateCoverageOptionRepository : Repository<PrivateCoverageOption>, IPrivateCoverageOptionRepository
    {
        public PrivateCoverageOptionRepository(Context context) : base(context)
        {
        }

        public async Task<List<PrivateCoverageOption>> GetAllPrivateCoverageOptions()
        {
            return _context.PrivateCoverageOption.ToList();
        }

        //This method is used to get a specific private coverage option based on the coverage amount, start date and insurance type.
        public async Task<PrivateCoverageOption> GetSpecificPrivateCoverageOption(decimal coverageAmount, DateTime startDate, InsuranceType insuranceType)
        {
            int year = startDate.Year; // Få året från startdatumet

            return await _context.PrivateCoverageOption
                .FirstOrDefaultAsync(x => x.CoverageAmount == coverageAmount && x.StartDate.Year == year && x.InsuranceType == insuranceType);
        }


    }
}
