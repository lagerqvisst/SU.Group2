using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<(bool Success, string Message, List<SellerStatistics> Statistics)> GetSellerStatistics(int year, List<InsuranceType>? insuranceTypes = null);
        Task<(bool Success, string Message, List<InsuranceStatistics> Statistics)> GetMonthlyInsuranceStatistics();
    }
}
