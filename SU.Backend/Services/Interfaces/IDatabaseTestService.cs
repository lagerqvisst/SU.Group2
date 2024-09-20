using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    public interface IDatabaseTestService
    {
        Task<(bool Success, string Message)> TestDbConnection();
        Task<(bool Success, string Message)> SeedInsuranceCoverages();

    }
}
