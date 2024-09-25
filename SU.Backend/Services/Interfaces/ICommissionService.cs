using SU.Backend.Models.Comissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    public interface ICommissionService
    {
        Task<(bool Success, string Message, List<Commission> Commissions)> GetAllCommissions(DateTime StartDate, DateTime EndDate);
    }
}
