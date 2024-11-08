using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Comissions;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances;

namespace SU.Backend.Database.Repositories;

/// <summary>
///     This class is responsible for implementing the methods defined in the IInsuranceRepository interface.
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
            .Where(i => i.InsuranceStatus == InsuranceStatus.Active && i.StartDate >= startDate &&
                        i.StartDate <= endDate)
            .ToList();
    }
    
    // This method is used to get all insurances that are active within a specific date range
    public async Task<List<Commission>> GetSellerCommissions(DateTime startDate, DateTime endDate)
    {
        // Fetch all sellers
        var sellers = await _context.Employees.Where(a => a.AgentNumber != null).ToListAsync();

        // Fetch active insurances within the specified date range
        var insurances = await _context.Insurances
            .Where(ins => ins.StartDate <= endDate && ins.EndDate >= startDate)
            .Include(ins => ins.Seller)
            .ToListAsync();

        // Group insurances by sellerId
        var insuranceGroups = insurances
            .GroupBy(ins => ins.SellerId)
            .ToDictionary(g => g.Key, g => g.Sum(ins => ins.Premium));

        // Left join sellers with insurance groups
        var commissions = sellers.Select(seller => new Commission
        {
            AgentNumber = seller.AgentNumber,
            SellerName = $"{seller.FirstName} {seller.LastName}",
            PersonalNumber = seller.PersonalNumber,
            CommissionAmount = insuranceGroups.ContainsKey(seller.EmployeeId)
                ? Commission.CalculateCommission(insuranceGroups[seller.EmployeeId])
                : "0.00 SEK", // No sales, no commission, formatted as string with "SEK"
            StartDate = startDate,
            EndDate = endDate
        }).ToList();

        return commissions;
    }

    //This method is used to get all insurances
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

    //This method is used to get all insurances by year
    public async Task<List<Insurance>> GetInsurancesByYear(int year)
    {
        return _context.Insurances
            .Include(i => i.Seller) // Include the seller
            .Where(i => i.StartDate.Year <= year &&
                        i.EndDate.Year >= year) // Filter by the given year
            .ToList();
    }

    //This method is used to get all insurances for invoicing
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
                            (i.PaymentPlan == PaymentPlan.Quarterly &&
                             (currentDate.Month - i.StartDate.Month) % 3 == 0) || // Fakturera var tredje månad
                            (i.PaymentPlan == PaymentPlan.SemiAnnual &&
                             (currentDate.Month - i.StartDate.Month) % 6 == 0) || // Fakturera var sjätte månad
                            (i.PaymentPlan == PaymentPlan.Annual &&
                             (currentDate.Month - i.StartDate.Month) % 12 == 0) // Fakturera var tolfte månad
                        ))
            .ToListAsync();
    }
}