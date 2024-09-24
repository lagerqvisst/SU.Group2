using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Models.Enums;
using SU.Backend.Models.Enums.Prospects;
using SU.Backend.Models.Insurances.Prospects;
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
                // Hämta privata kunder
                _logger.LogInformation("Getting private customers with >0 && <2 insurance (1 insurance only)");
                var privateCustomers = await _unitOfWork.PrivateCustomers.GetProspectDataForPrivateCustomers();
                if (privateCustomers == null || !privateCustomers.Any())
                {
                    _logger.LogInformation("No private customer prospects found");
                }
                _logger.LogInformation($"Found {privateCustomers.Count} private customer prospects");

                // Hämta företagskunder
                _logger.LogInformation("Getting company customers with >0 && <2 insurance (1 insurance only)");
                var companyCustomers = await _unitOfWork.CompanyCustomers.GetProspectDataForCompanyCustomers();
                if (companyCustomers == null || !companyCustomers.Any())
                {
                    _logger.LogInformation("No company customer prospects found");
                }
                _logger.LogInformation($"Found {companyCustomers.Count} company customer prospects");

                // Kombinera prospektlistor
                List<Prospect> prospects = new List<Prospect>();

                // Behandla privata kunder
                foreach (var privateCustomer in privateCustomers)
                {
                    _logger.LogInformation($"Checking if prospect for PrivateCustomer ID {privateCustomer.PrivateCustomerId} already exists in the database");
                    if (!await _unitOfWork.Prospects.ProspectExists(privateCustomer.PrivateCustomerId, null))
                    {
                        prospects.Add(new Prospect
                        {
                            ProspectStatus = ProspectStatus.NotContacted,
                            ProspectType = ProspectType.Private,
                            PrivateCustomer = privateCustomer
                        });
                    }
                    else
                    {
                        _logger.LogInformation($"Prospect for PrivateCustomer {privateCustomer.PrivateCustomerId} already exists, skipping...");
                    }
                }

                // Behandla företagskunder
                foreach (var companyCustomer in companyCustomers)
                {
                    _logger.LogInformation($"Checking if prospect for CompanyCustomer ID {companyCustomer.CompanyCustomerId} already exists in the database");
                    if (!await _unitOfWork.Prospects.ProspectExists(null, companyCustomer.CompanyCustomerId))
                    {
                        prospects.Add(new Prospect
                        {
                            ProspectStatus = ProspectStatus.NotContacted,
                            ProspectType = ProspectType.Company,
                            CompanyCustomer = companyCustomer
                        });
                    }
                    else
                    {
                        _logger.LogInformation($"Prospect for CompanyCustomer {companyCustomer.CompanyCustomerId} already exists, skipping...");
                    }
                }

                // Spara prospekt till databasen
                if (prospects.Any())
                {
                    _logger.LogInformation($"Adding {prospects.Count} prospect(s) to the database");
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


        public async Task<(bool Success, string Message)> TestAssignSellerToProspect()
        {
            _logger.LogInformation("Updating prospect status");

            try
            {
                _logger.LogInformation("Getting 1 Seller from Employee Repository");
                var seller = await _unitOfWork.Employees.GetEmployeeByRole(EmployeeType.OutsideSales);

                if (seller == null)
                {
                    _logger.LogInformation("No sellers found");
                    return (false, "No sellers found");
                }

                _logger.LogInformation("Getting all prospects from database");
                var prospects = await _unitOfWork.Prospects.GetAllProspects();

                if(prospects == null)
                {
                    _logger.LogInformation("No prospects found");
                    return (false, "No prospects found");
                }

                var privateProspects = prospects.Where(p => p.ProspectType == ProspectType.Private).ToList();
                var companyProspects = prospects.Where(p => p.ProspectType == ProspectType.Company).ToList();

                _logger.LogInformation($"#{privateProspects.Count} private prospect(s) found");
                _logger.LogInformation($"#{companyProspects.Count} company prospect(s) found");

                _logger.LogInformation("Assigning Seller to prospects");

                foreach (var prospect in prospects)
                {
                    if (prospect.Seller == null)
                    {
                        prospect.Seller = seller;
                        prospect.AssignedAgentNumber = seller.AgentNumber;

                        await _unitOfWork.Prospects.UpdateAsync(prospect);
                        _logger.LogInformation($"Seller assigned to prospect id: {prospect.ProspectId}");
                    }
                }

                _logger.LogInformation("Saving changes to database");
                await _unitOfWork.SaveChangesAsync();

                return (true, "Seller assigned to prospects");

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error assigning seller to prospects");
                return (false, "Error assigning seller to prospects");

            }
        }
    }
}

