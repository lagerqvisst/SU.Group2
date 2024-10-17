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
