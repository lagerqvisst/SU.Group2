using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    /// <summary>
    /// This interface is responsible for defining the methods that the StatisticsService class must implement.
    /// </summary>
    public interface IStatisticsService
    {
        Task<(bool success, string message, List<SellerStatistics> statistics)> GetSellerStatistics(int year, List<InsuranceType>? insuranceTypes = null);
        Task<(bool success, string message, List<InsuranceStatistics> statistics)> GetMonthlyInsuranceStatistics();
    }
}
