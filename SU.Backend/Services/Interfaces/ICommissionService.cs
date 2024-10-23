using SU.Backend.Models.Comissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    /// <summary>
    /// This interface is responsible for defining the methods that the CommissionService class must implement.
    /// </summary>
    public interface ICommissionService
    {
        Task<(bool success, string message, List<Commission> commissions)> GetAllCommissions(DateTime startDate, DateTime endDate);
    }
}
