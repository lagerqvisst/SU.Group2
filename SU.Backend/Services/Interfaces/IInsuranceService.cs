using SU.Backend.Models.Customers;
using SU.Backend.Models.Insurances.Coverage;
using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    public interface IInsuranceService
    {
        Task<(bool Success, string Message)> CreateTestInsurance();

        Task<(bool Success, string Message)> RemoveAllInsurances();

    }
}
