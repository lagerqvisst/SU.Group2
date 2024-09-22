using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurance.Coverage;
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

            // Logga de inkommande värdena
            Console.WriteLine($"Coverage Amount: {coverageAmount}, Start Date: {startDate.ToShortDateString()}, Year: {year}, Insurance Type: {insuranceType}");

            // Hämta alla alternativ som matchar det givna året och försäkringstypen
            var matchingOptions = await _context.PrivateCoverageOption
                .Where(x => x.StartDate.Year == year && x.InsuranceType == insuranceType)
                .ToListAsync();

            // Logga alla alternativ för att se vad som finns
            foreach (var option in matchingOptions)
            {
                Console.WriteLine($"Found Option - Amount: {option.CoverageAmount}, Start Date: {option.StartDate.ToShortDateString()}, Type: {option.InsuranceType}");
            }

            // Försök hämta den specifika täckningsalternativet
            return await _context.PrivateCoverageOption
                .FirstOrDefaultAsync(x => x.CoverageAmount == coverageAmount && x.StartDate.Year == year && x.InsuranceType == insuranceType);
        }


    }
}
