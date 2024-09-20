using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    public interface IInsuranceService
    {
        Task<(bool Success, string Message)> SeedPrivateInsuranceOptions();
    }
}
