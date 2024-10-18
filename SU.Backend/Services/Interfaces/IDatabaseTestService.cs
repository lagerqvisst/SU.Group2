using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    /// <summary>
    /// This interface is responsible for defining the methods that the DatabaseTestService class must implement.
    /// </summary>
    public interface IDatabaseTestService
    {
        Task<(bool Success, string Message)> TestDbConnection();

        Task<(bool Success, string Message)> RecreateDb();

        Task<(bool Success, string Message)> SeedEmployeeOrganisation();
    }
}
