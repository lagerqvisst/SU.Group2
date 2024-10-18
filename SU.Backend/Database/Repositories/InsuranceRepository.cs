using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Comissions;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    /// <summary>
    /// This class is responsible for implementing the methods defined in the IInsuranceRepository interface.
    /// </summary>
    public class InsuranceRepository : Repository<Insurance>, IIunsuranceRepository
    {
        public InsuranceRepository(Context context) : base(context)
        {
        }

        //This method is used to get all active insurances
        //Used in multiple services methods
        public async Task<List<Insurance>> GetAllActiveInsurances()
        {
            return await _context.Insurances
                .Include(s => s.Seller)
                .Include(c => c.InsurancePolicyHolder)
                    .ThenInclude(p => p.PrivateCustomer)  
                .Include(c => c.InsurancePolicyHolder)
                    .ThenInclude(c => c.CompanyCustomer)  
                .Where(i => i.InsuranceStatus == InsuranceStatus.Active)
                .ToListAsync();
        }

        //This method is used to get all active insurances within a specific date range
        public async Task<List<Insurance>> GetActiveInsurancesInDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Insurances
                .Include(s => s.Seller)
                .Where(i => i.InsuranceStatus == InsuranceStatus.Active && i.StartDate >= startDate && i.StartDate <= endDate)
                .ToListAsync();
        }

        //TODO: Include left join for sellers who did not sell any insurances
        public async Task<List<Commission>> GetSellerCommissions(DateTime startDate, DateTime endDate)
        {
            // Get active insurances within the specified date range
            var insurances = await GetActiveInsurancesInDateRange(startDate, endDate);

            if (insurances == null || !insurances.Any())
            {
                return new List<Commission>();
            }

            // Group by seller and calculate commissions
            var commissions = insurances
                .GroupBy(ins => ins.Seller)
                .Select(group => new Commission
                {
                    Seller = group.Key,
                    SellerName = group.Key.FirstName + " " + group.Key.LastName,
                    PersonalNumber = group.Key.PersonalNumber,
                    CommissionAmount = Commission.CalculateCommission(group.Sum(ins => ins.Premium)),
                    StartDate = startDate,
                    EndDate = endDate
                })
                .ToList();

            return commissions;
        }

        public async Task<List<Insurance>> GetAllInsurances()
        {
            return await _context.Insurances
                .Include(i => i.InsurancePolicyHolder)
                    .ThenInclude(p => p.PrivateCustomer)
                .Include(i => i.InsuranceCoverage)
                    .ThenInclude(ic => ic.PrivateCoverage)
                        .ThenInclude(pc => pc.PrivateCoverageOption) // Inkludera PrivateCoverageOption
                .Include(i => i.InsuranceCoverage)
                    .ThenInclude(ic => ic.PrivateCoverage)
                .Include(i => i.InsuranceAddons)
                .ToListAsync();
        }

        public async Task<List<Insurance>> GetInsurancesByYear(int year)
        {
            return await _context.Insurances
                .Include(i => i.Seller) // Include the seller
                .Where(i => i.StartDate.Year <= year && (i.EndDate == null || i.EndDate.Year >= year)) // Filter by the given year
                .ToListAsync();
        }

        //Not sure if this is or will be used anywhere
        public async Task<Insurance> GetInsuranceById(int id)
        {
            return await _context.Insurances
                .Include(i => i.InsurancePolicyHolder)
                    .ThenInclude(p => p.PrivateCustomer)
                .Include(i => i.InsuranceCoverage)
                    .ThenInclude(ic => ic.PrivateCoverage)
                        .ThenInclude(pc => pc.PrivateCoverageOption) 
                .Include(i => i.InsuranceCoverage)
                    .ThenInclude(ic => ic.PrivateCoverage)
                .Include(i => i.InsuranceAddons)
                .FirstOrDefaultAsync(i => i.InsuranceId == id);
        }


    }

}
