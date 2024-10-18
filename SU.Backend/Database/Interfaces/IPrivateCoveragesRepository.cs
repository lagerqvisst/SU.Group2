using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Interfaces
{
    /// <summary>
    /// This interface is responsible for defining the methods that the PrivateCoveragesRepository class must implement.
    /// </summary>
    public interface IPrivateCoveragesRepository
    {
        Task<List<PrivateCoverage>> GetAllPrivateCoverages();
    }
}
