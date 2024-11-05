using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Statistics;
using SU.Backend.Services.Interfaces;

namespace SU.Backend.Services;

/// <summary>
///     This service class is responsible for fetching and aggregating statistics
///     related to sellers and insurances, including premium calculations and sales performance.
///     It communicates with the repository layer to query necessary data.
/// </summary
public class StatisticsService : IStatisticsService
{
    private readonly ILogger<StatisticsService> _logger;
    private readonly UnitOfWork _unitOfWork;

    public StatisticsService(ILogger<StatisticsService> logger, UnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool success, string message, List<SellerStatistics> statistics)> GetSellerStatistics(int year,
        List<InsuranceType>? insuranceTypes = null)
    {
        try
        {
            // Step 1: Get all employees who are sellers (inside or outside sales roles)
            var salesEmployees = await _unitOfWork.Employees.GetSalesEmployees();

            // Step 2: Retrieve insurances for the specified year and types, with related seller information
            var insurancesQuery =
                await _unitOfWork.Insurances
                    .GetInsurancesByYear(year); // Ensure this method retrieves insurances for the whole year

            if (insuranceTypes != null && insuranceTypes.Any())
                insurancesQuery =
                    insurancesQuery.Where(i => insuranceTypes.Contains(i.InsuranceType)).ToList(); // Filter in-memory

            // Step 3: Group by seller and aggregate monthly data
            var groupedData = salesEmployees.GroupJoin(
                    insurancesQuery,
                    seller => seller.EmployeeId,
                    insurance => insurance.SellerId,
                    (seller, insurances) => new
                    {
                        seller1 = seller,
                        monthlySales = Enumerable.Range(1, 12).Select(month =>
                        {
                            // Initialize counts for each insurance type to 0
                            var insuranceCounts = Enum.GetValues(typeof(InsuranceType))
                                .Cast<InsuranceType>()
                                .ToDictionary(type => type, type => 0);

                            // Populate counts for existing sales in that month
                            foreach (var group in insurances
                                         .Where(i => i.StartDate.Month == month)
                                         .GroupBy(i => i.InsuranceType))
                                insuranceCounts[group.Key] = group.Count();

                            return new MonthlySalesData
                            {
                                Month = month,
                                InsuranceSalesCounts = insuranceCounts
                            };
                        }).ToList()
                    })
                .Select(data => new SellerStatistics
                {
                    SellerName = data.seller1.FirstName + " " + data.seller1.LastName,
                    AgentNumber = data.seller1.AgentNumber,
                    MonthlySales = data.monthlySales,
                    TotalYearlySales = data.monthlySales.Sum(m => m.TotalSales),
                    AverageMonthlySales = data.monthlySales.Average(m => m.TotalSales)
                })
                .ToList();


            return (true, "Success", groupedData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting seller statistics");
            return (false, "An error occurred while fetching seller statistics", new List<SellerStatistics>());
        }
    }

    public async Task<(bool success, string message, List<SellerStatistics> statistics)> GetActiveSellerStatistics(
        int year, List<InsuranceType>? insuranceTypes = null)
    {
        try
        {
            // Steg 1: Hämta alla säljare (inne- eller utesäljare)
            var salesEmployees = await _unitOfWork.Employees.GetSalesEmployees();

            // Steg 2: Hämta försäkringar för det angivna året och typer, med relaterad säljare
            var insurancesQuery = await _unitOfWork.Insurances.GetInsurancesByYear(year);

            if (insuranceTypes != null && insuranceTypes.Any())
                insurancesQuery = insurancesQuery.Where(i => insuranceTypes.Contains(i.InsuranceType)).ToList();

            // Steg 3: Gruppera per säljare och aggregera månatlig data
            var groupedData = salesEmployees.GroupJoin(
                    insurancesQuery,
                    seller => seller.EmployeeId,
                    insurance => insurance.SellerId,
                    (seller, insurances) => new
                    {
                        Seller = seller,
                        MonthlySales = Enumerable.Range(1, 12).Select(month =>
                        {
                            var insuranceCounts = Enum.GetValues(typeof(InsuranceType))
                                .Cast<InsuranceType>()
                                .ToDictionary(type => type, type => 0);

                            foreach (var group in insurances
                                         .Where(i => i.StartDate.Month == month)
                                         .GroupBy(i => i.InsuranceType))
                                insuranceCounts[group.Key] = group.Count();

                            var totalSales = insuranceCounts.Values.Sum(); // Räknar total försäljning för månaden
                            Console.WriteLine(
                                $"Seller: {seller.FirstName} {seller.LastName}, Month: {month}, TotalSales: {totalSales}"); // Logg

                            return new MonthlySalesData
                            {
                                Month = month,
                                InsuranceSalesCounts = insuranceCounts
                            };
                        }).ToList()
                    })
                .Where(data =>
                    data.MonthlySales.Any(m => m.TotalSales > 0)) // Filtrera enbart säljare med försäljningar
                .Select(data => new SellerStatistics
                {
                    SellerName = data.Seller.FirstName + " " + data.Seller.LastName,
                    AgentNumber = data.Seller.AgentNumber,
                    MonthlySales = data.MonthlySales,
                    TotalYearlySales = data.MonthlySales.Sum(m => m.TotalSales),
                    AverageMonthlySales = data.MonthlySales.Average(m => m.TotalSales)
                })
                .ToList();

            return (true, "Success", groupedData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting active seller statistics");
            return (false, "An error occurred while fetching active seller statistics", new List<SellerStatistics>());
        }
    }


    public async Task<(bool success, string message, List<InsuranceStatistics> statistics)>
        GetMonthlyInsuranceStatistics()
    {
        _logger.LogInformation("Fetching monthly insurance statistics");

        try
        {
            // Step 1: Fetch all active insurances
            var activeInsurances = await _unitOfWork.Insurances.GetAllActiveInsurances();

            // Step 2: Create a list of all insurance types from the enum
            var allInsuranceTypes = Enum.GetValues(typeof(InsuranceType)).Cast<InsuranceType>();

            // Step 3: Group insurances by type and month, and calculate total policies and premium
            var groupedStatistics = activeInsurances
                .GroupBy(i => new { i.InsuranceType, i.StartDate.Month })
                .Select(g => new InsuranceStatistics
                {
                    InsuranceType = g.Key.InsuranceType.ToString(),
                    Month = g.Key.Month,
                    TotalPolicies = g.Count(),
                    TotalPremium = g.Sum(i => i.Premium)
                })
                .ToList();

            // Step 4: Ensure all insurance types are included in the statistics
            var allStatistics = new List<InsuranceStatistics>();
            foreach (var type in allInsuranceTypes)
                for (var month = 1; month <= 12; month++)
                {
                    var existingStat = groupedStatistics
                        .FirstOrDefault(stat => stat.InsuranceType == type.ToString() && stat.Month == month);

                    if (existingStat != null)
                        allStatistics.Add(existingStat);
                    else
                        // Add a zero-filled entry for types without sales
                        allStatistics.Add(new InsuranceStatistics
                        {
                            InsuranceType = type.ToString(),
                            Month = month,
                            TotalPolicies = 0,
                            TotalPremium = 0m
                        });
                }

            // Return the statistics
            return (true, "Success", allStatistics.OrderBy(stat => stat.Month).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while fetching insurance statistics");
            return (false, "An error occurred while fetching insurance statistics", new List<InsuranceStatistics>());
        }
    }

    public async Task<(bool success, string message, SellerStatistics statistics)> GetSellerStatisticsBySeller(int year,
        Employee seller)
    {
        try
        {
            // Hämta försäkringar för den specifika säljaren och året
            var insurancesQuery = await _unitOfWork.Insurances.GetInsurancesByYear(year);
            var sellerInsurances = insurancesQuery.Where(i => i.SellerId == seller.EmployeeId).ToList();

            // Gruppera försäljning per månad
            var monthlySales = Enumerable.Range(1, 12).Select(month =>
            {
                var insuranceCounts = Enum.GetValues(typeof(InsuranceType))
                    .Cast<InsuranceType>()
                    .ToDictionary(type => type, type => 0);

                foreach (var group in sellerInsurances
                             .Where(i => i.StartDate.Month == month)
                             .GroupBy(i => i.InsuranceType))
                    insuranceCounts[group.Key] = group.Count();

                return new MonthlySalesData
                {
                    Month = month,
                    InsuranceSalesCounts = insuranceCounts
                };
            }).ToList();

            // Skapa SellerStatistics objekt för den specifika säljaren
            var sellerStatistics = new SellerStatistics
            {
                SellerName = seller.FirstName + " " + seller.LastName,
                AgentNumber = seller.AgentNumber,
                MonthlySales = monthlySales,
                TotalYearlySales = monthlySales.Sum(m => m.TotalSales),
                AverageMonthlySales = monthlySales.Average(m => m.TotalSales)
            };

            return (true, "Success", sellerStatistics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching statistics for seller {seller.FirstName} {seller.LastName}");
            return (false, "An error occurred while fetching seller statistics", null);
        }
    }
}