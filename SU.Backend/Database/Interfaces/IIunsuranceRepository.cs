using SU.Backend.Models.Comissions;
using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Interfaces
{
    /// <summary>
    /// This interface is responsible for defining the methods that the InsuranceRepository class must implement.
    /// </summary>
    public interface IIunsuranceRepository
    {
        Task<List<Insurance>> GetAllInsurances();
        Task<Insurance> GetInsuranceById(int id);
        Task<List<Commission>> GetSellerCommissions(DateTime startDate, DateTime endDate);
        Task<List<Insurance>> GetActiveInsurancesInDateRange(DateTime startDate, DateTime endDate);
        Task<List<Insurance>> GetAllActiveInsurances();
        Task<List<Insurance>> GetInsurancesByYear(int year);


    }
}
