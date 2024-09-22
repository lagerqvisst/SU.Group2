using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Models.Enums;
using SU.Backend.Models.Insurance.Prospects;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    public class ProspectService : IProspectService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<ProspectService> _logger;

        public ProspectService(UnitOfWork unitOfWork, ILogger<ProspectService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<(bool Success, string Message, List<Prospect> prospects)> IdentifyProspects()
        {
            _logger.LogInformation("Identifying prospects");
            try
            {
                _logger.LogInformation("Getting private customers with >0 && <2 insurance (1 insurance only)");
                var privateCustomers = await _unitOfWork.PrivateCustomers.GetProspectDataForPrivateCustomers();

                if (privateCustomers == null || !privateCustomers.Any())
                {
                    _logger.LogInformation("No prospects found");
                    return (false, "No prospects found", null);
                }

                _logger.LogInformation($"{privateCustomers.Count} Prospects found");

                List<Prospect> prospects = new List<Prospect>();

                foreach (var privateCustomer in privateCustomers)
                {
                    _logger.LogInformation($"Checking if prospect for PrivateCustomer {privateCustomer.PrivateCustomerId} already exists in the database");
                    if (!await _unitOfWork.Prospects.ProspectExists(privateCustomer.PrivateCustomerId, null))
                    {
                        prospects.Add(new Prospect
                        {
                            ProspectStatus = ProspectStatus.NotContacted,
                            PrivateCustomer = privateCustomer
                        });
                    }
                    else
                    {
                        _logger.LogInformation($"Prospect for PrivateCustomer {privateCustomer.PrivateCustomerId} already exists, skipping...");
                    }
                }

                if (prospects.Any())
                {
                    _logger.LogInformation($"Adding {prospects.Count} prospects to the database");
                    await _unitOfWork.Prospects.AddRangeAsync(prospects);
                    await _unitOfWork.SaveChangesAsync();

                    return (true, $"{prospects.Count} Prospects identified", prospects);
                }
                else
                {
                    _logger.LogInformation("No new prospects to add to the database");
                    return (false, "No new prospects identified", null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error identifying prospects");
                return (false, "Error identifying prospects", null);
            }
        }

    }
}

