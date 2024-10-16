﻿using Microsoft.Extensions.Logging;
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

        public async Task<(bool Success, string Message, List<SellerStatistics> Statistics)> GetSellerStatistics(int year, List<InsuranceType>? insuranceTypes = null)
        {
            _logger.LogInformation($"Getting seller statistics for year {year} and insurance types: {string.Join(", ", insuranceTypes)}");

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

                return (true, "Success", groupedData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting seller statistics");
                return (false, "An error occurred while fetching seller statistics", new List<SellerStatistics>());
            }

        }

        public async Task<(bool Success, string Message, List<InsuranceStatistics> Statistics)> GetMonthlyInsuranceStatistics()
        {
            _logger.LogInformation("Fetching monthly insurance statistics");

            try
            {
                // Steg 1: Hämta alla aktiva försäkringar
                var activeInsurances = await _unitOfWork.Insurances.GetAllActiveInsurances();

                // Kontrollera om det finns några försäkringar
                if (activeInsurances == null || !activeInsurances.Any())
                {
                    _logger.LogInformation("No active insurances found");
                    return (false, "No active insurances found", new List<InsuranceStatistics>());
                }

                // Steg 2: Grupper försäkringar efter försäkringstyp och månad
                var statistics = activeInsurances
                    .GroupBy(i => new { i.InsuranceType, Month = i.StartDate.Month })
                    .Select(g => new InsuranceStatistics
                    {
                        InsuranceType = g.Key.InsuranceType.ToString(),
                        Month = g.Key.Month,
                        TotalPolicies = g.Count(),
                        TotalPremium = g.Sum(i => i.Premium)
                    })
                    .OrderBy(stat => stat.Month)  // Sortera efter månad
                    .ToList();

                // Steg 3: Returnera resultat
                return (true, "Success", statistics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching insurance statistics");
                return (false, "An error occurred while fetching insurance statistics", new List<InsuranceStatistics>());
            }
        }



    }
}
