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
        Task<List<SellerStatistics>> GetSellerStatistics(int year, List<InsuranceType>? insuranceTypes = null);

        void PrintSellerStatistics(List<SellerStatistics> sellerStatistics, int year, List<InsuranceType> insuranceTypes);
    }
}
