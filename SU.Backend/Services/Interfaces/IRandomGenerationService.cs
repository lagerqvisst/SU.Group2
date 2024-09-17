using SU.Backend.Helper;
using SU.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SU.Backend.Services.RandomGenerationService;

namespace SU.Backend.Services.Interfaces
{
    public interface IRandomGenerationService
    {
        Task<(bool Success, RandomUserApiResponse RandomUserInfo)> GenerateSingleRandomUser();
        Task<(bool Success, List<RandomUserApiResponse> RandomUserInfo)> GenerateMultipleRandomUsers(int numberOfUsers = 10);
    }
}
