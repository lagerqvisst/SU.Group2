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
    /// <summary>
    /// This class is responsible for handling the business logic for commissions.
    /// </summary>
    public class CommissionService : ICommissionService
    {
        private readonly ILogger<CommissionService> _logger;
        private readonly UnitOfWork _unitOfWork;

        public CommissionService(ILogger<CommissionService> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        // Method to get all commissions, most of the heavy lifting is done in the repoisitory method.
        public async Task<(bool success, string message, List<Commission> commissions)> GetAllCommissions(DateTime startDate, DateTime endDate)
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
