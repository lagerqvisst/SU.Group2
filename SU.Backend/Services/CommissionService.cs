using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Models.Comissions;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    public class CommissionService : ICommissionService
    {
        private readonly ILogger<CommissionService> _logger;
        private readonly UnitOfWork _unitOfWork;

        public CommissionService(ILogger<CommissionService> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<(bool Success, string Message, List<Commission> Commissions)> GetAllCommissions(DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation("Getting all commissions");
            try
            {
                var commissions = await _unitOfWork.Insurances.GetSellerCommissions(startDate, endDate);
                if (commissions == null || !commissions.Any())
                {
                    _logger.LogInformation("No commissions found");
                    return (false, "No commissions found", null);
                }
                _logger.LogInformation($"Found {commissions.Count} commissions");

                #region Temp Console Prints
                // Console prints to display the commissions until the frontend is ready
                Console.WriteLine("=== Commission Report ===");
                Console.WriteLine($"{"Agent Number",-15}{"Name",-25}{"Personal Number",-20}{"Commission Amount",-20}{"Period",-30}");
                Console.WriteLine(new string('=', 110));

                foreach (var commission in commissions)
                {
                    Console.WriteLine(
                        $"{commission.Seller.AgentNumber,-15}" +
                        $"{commission.SellerName,-25}" +
                        $"{commission.PersonalNumber,-20}" +
                        $"{commission.CommissionAmount,-20:C}" +  // Display as currency
                        $"{commission.StartDate.ToShortDateString()} - {commission.EndDate.ToShortDateString(),-30}");
                }

                Console.WriteLine(new string('=', 110));
                Console.WriteLine($"Total Commissions: {commissions.Count}");
                Console.ReadLine();
                #endregion
                return (true, "Commissions found", commissions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting commissions");
                return (false, "Error getting commissions", null);
            }
        }

    }
}
