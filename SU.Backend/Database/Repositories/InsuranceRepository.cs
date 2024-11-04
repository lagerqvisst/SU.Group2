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
            return _context.Insurances
                .Include(s => s.Seller)
                .Include(c => c.InsurancePolicyHolder)
                    .ThenInclude(p => p.PrivateCustomer)  
                .Include(c => c.InsurancePolicyHolder)
                    .ThenInclude(c => c.CompanyCustomer)  
                .Where(i => i.InsuranceStatus == InsuranceStatus.Active)
                .ToList();
        }

        //This method is used to get all active insurances within a specific date range
        public async Task<List<Insurance>> GetActiveInsurancesInDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Insurances
                .Include(s => s.Seller)
                .Where(i => i.InsuranceStatus == InsuranceStatus.Active && i.StartDate >= startDate && i.StartDate <= endDate)
                .ToList();
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
                    AgentNumber = group.Key.AgentNumber,
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
            return _context.Insurances
                .Include(i => i.InsurancePolicyHolder)
                    .ThenInclude(p => p.PrivateCustomer)
                .Include(i => i.InsuranceCoverage)
                    .ThenInclude(ic => ic.PrivateCoverage)
                        .ThenInclude(pc => pc.PrivateCoverageOption) // Include PrivateCoverageOption
                .Include(i => i.InsuranceCoverage)
                    .ThenInclude(ic => ic.PrivateCoverage)
                .Include(i => i.InsurancePolicyHolder)
                    .ThenInclude(p => p.CompanyCustomer)
                .Include(i => i.InsuranceAddons)
                .ToList();
        }

        public async Task<List<Insurance>> GetInsurancesByYear(int year)
        {
            return  _context.Insurances
                .Include(i => i.Seller) // Include the seller
                .Where(i => i.StartDate.Year <= year && (i.EndDate == null || i.EndDate.Year >= year)) // Filter by the given year
                .ToList();
        }

        //Not sure if this is or will be used anywhere
        public async Task<Insurance> GetInsuranceById(int id)
        {
            return _context.Insurances
                .Include(i => i.InsurancePolicyHolder)
                    .ThenInclude(p => p.PrivateCustomer)
                .Include(i => i.InsuranceCoverage)
                    .ThenInclude(ic => ic.PrivateCoverage)
                        .ThenInclude(pc => pc.PrivateCoverageOption) 
                .Include(i => i.InsuranceCoverage)
                    .ThenInclude(ic => ic.PrivateCoverage)
                .Include(i => i.InsuranceAddons)
                .FirstOrDefault(i => i.InsuranceId == id);
        }

        public async Task<List<Insurance>> GetInsurancesForInvoicing(DateTime currentDate)
        {
            return await _context.Insurances
                .Include(i => i.InsurancePolicyHolder)
                    .ThenInclude(p => p.PrivateCustomer)
                .Include(i => i.InsurancePolicyHolder)
                    .ThenInclude(p => p.CompanyCustomer)
                .Include(i => i.InsuranceCoverage)
                .Where(i => i.StartDate <= currentDate && // Försäkringar som har startat
                            i.InsuranceStatus == InsuranceStatus.Active && // Endast aktiva försäkringar
                            (
                                i.PaymentPlan == PaymentPlan.Monthly || // Fakturera varje månad
                                (i.PaymentPlan == PaymentPlan.Quarterly && ((currentDate.Month - i.StartDate.Month) % 3 == 0)) || // Fakturera var tredje månad
                                (i.PaymentPlan == PaymentPlan.SemiAnnual && ((currentDate.Month - i.StartDate.Month) % 6 == 0)) || // Fakturera var sjätte månad
                                (i.PaymentPlan == PaymentPlan.Annual && ((currentDate.Month - i.StartDate.Month) % 12 == 0)) // Fakturera var tolfte månad
                            ))
                .ToListAsync();
        }




    }

}
