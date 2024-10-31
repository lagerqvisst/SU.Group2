using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances;
using SU.Backend.Models.Statistics;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    /// <summary>
    /// This service class is responsible for fetching and aggregating statistics 
    /// related to sellers and insurances, including premium calculations and sales performance. 
    /// It communicates with the repository layer to query necessary data.
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

        public async Task<(bool success, string message, List<SellerStatistics> statistics)> GetSellerStatistics(int year, List<InsuranceType>? insuranceTypes = null)
        {
           // _logger.LogInformation($"Getting seller statistics for year {year} and insurance types: {string.Join(", ", insuranceTypes)}");

            try
            {
                // Step 1: Get all employees who are sellers (inside or outside sales roles)
                var salesEmployees = await _unitOfWork.Employees.GetSalesEmployees();

                // Step 2: Retrieve insurances for the specified year and types, with related seller information
                var insurancesQuery = await _unitOfWork.Insurances.GetInsurancesByYear(year); // Ensure this method retrieves insurances for the whole year

                if (insuranceTypes != null && insuranceTypes.Any())
                {
                    insurancesQuery = insurancesQuery.Where(i => insuranceTypes.Contains(i.InsuranceType)).ToList(); // Filter in-memory
                }

                // Step 3: Group by seller and aggregate monthly data
                var groupedData = salesEmployees.GroupJoin(
                    insurancesQuery,
                    seller => seller.EmployeeId,
                    insurance => insurance.SellerId,
                    (seller, insurances) => new
                    {
                        seller1 = seller,
                        monthlySales = Enumerable.Range(1, 12).Select(month => new MonthlySalesData
                        {
                            Month = month,
                            InsuranceSalesCounts = insurances
                                .Where(i => i.StartDate.Month == month)
                                .GroupBy(i => i.InsuranceType)
                                .ToDictionary(group => group.Key, group => group.Count())
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

        public async Task<(bool success, string message, List<InsuranceStatistics> statistics)> GetMonthlyInsuranceStatistics()
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
                    .GroupBy(i => new { i.InsuranceType, Month = i.StartDate.Month })
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
                {
                    for (int month = 1; month <= 12; month++)
                    {
                        var existingStat = groupedStatistics
                            .FirstOrDefault(stat => stat.InsuranceType == type.ToString() && stat.Month == month);

                        if (existingStat != null)
                        {
                            allStatistics.Add(existingStat);
                        }
                        else
                        {
                            // Add a zero-filled entry for types without sales
                            allStatistics.Add(new InsuranceStatistics
                            {
                                InsuranceType = type.ToString(),
                                Month = month,
                                TotalPolicies = 0,
                                TotalPremium = 0m
                            });
                        }
                    }
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

    }
}
