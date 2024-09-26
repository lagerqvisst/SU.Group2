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
    public class StatisticsService : IStatisticsService
    {
        private readonly ILogger<StatisticsService> _logger;
        private readonly UnitOfWork _unitOfWork;

        public StatisticsService(ILogger<StatisticsService> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SellerStatistics>> GetSellerStatistics(int year, List<InsuranceType>? insuranceTypes = null)
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
                    Seller = seller,
                    MonthlySales = Enumerable.Range(1, 12).Select(month => new MonthlySalesData
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
                    SellerName = data.Seller.FirstName + " " + data.Seller.LastName,
                    AgentNumber = data.Seller.AgentNumber,
                    MonthlySales = data.MonthlySales,
                    TotalYearlySales = data.MonthlySales.Sum(m => m.TotalSales),
                    AverageMonthlySales = data.MonthlySales.Average(m => m.TotalSales)
                })
                .ToList();

            return groupedData;
        }







        public void PrintSellerStatistics(List<SellerStatistics> sellerStatistics, int year, List<InsuranceType> insuranceTypes)
        {
            // Mapping of InsuranceType enum to display names
            var insuranceTypeNames = insuranceTypes.ToDictionary(type => type, type => type.ToString().Replace("Insurance", "").Replace("AndHealth", ""));

            // Print table headers
            Console.WriteLine($"(exempel på tabell för valda försäkringstyper)\n");
            Console.Write($"{"Säljare/försäkring",-20} ");

            // Print month names header
            for (int month = 1; month <= 12; month++)
            {
                string monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                Console.Write($"{monthName,-10}");
            }

            // Print insurance types header for each month
            foreach (var type in insuranceTypeNames)
            {
                Console.Write($"{type.Value,-10}");
            }

            Console.Write("Total      ");
            Console.WriteLine("Hela året");

            // Loop through each seller to print their statistics
            foreach (var seller in sellerStatistics)
            {
                Console.Write($"{seller.SellerName,-20}");

                int yearlyTotal = 0; // Track total sales for the year

                // Loop through all 12 months
                for (int month = 1; month <= 12; month++)
                {
                    var monthlyData = seller.MonthlySales.FirstOrDefault(m => m.Month == month);

                    if (monthlyData != null)
                    {
                        int totalMonthlySales = 0;

                        // Print the sales data for each insurance type dynamically
                        foreach (var type in insuranceTypes)
                        {
                            int count = monthlyData.InsuranceSalesCounts.TryGetValue(type, out var insuranceCount) ? insuranceCount : 0;
                            totalMonthlySales += count;
                            Console.Write($"{count,-10}");
                        }

                        yearlyTotal += totalMonthlySales;
                        Console.Write($"{totalMonthlySales,-10}");
                    }
                }

                // Print yearly total and monthly average
                int averageMonthlySales = seller.MonthlySales.Count > 0 ? yearlyTotal / seller.MonthlySales.Count : 0;
                Console.WriteLine($" {yearlyTotal,-10} {averageMonthlySales,-10}");
            }

            Console.ReadLine();
        }



    }
}
