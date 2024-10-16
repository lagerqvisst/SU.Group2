using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Models.Employees;
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
                            PrivateCustomer = privateCustomer,
                            ContactNote = "test"
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


        public async Task<(bool Success, string Message)> AssignSellerToSpecificProspect(Employee employee, Prospect prospect)
        {
            _logger.LogInformation("Checking if employee has the correct role assignments");

            try
            {
                // Kontrollera om anställd har antingen OutsideSales eller InsideSales roller
                var hasValidRole = employee.RoleAssignments.Any(role =>
                    role.Role == EmployeeType.OutsideSales || role.Role == EmployeeType.InsideSales);

                if (!hasValidRole)
                {
                    _logger.LogInformation("Employee does not have the correct role (OutsideSales or InsideSales)");
                    return (false, "Employee does not have the correct role (OutsideSales or InsideSales)");
                }

                _logger.LogInformation("Employee has a valid role assignment");

                // Kontrollera om prospektet redan har en säljare
                if (prospect.Seller != null)
                {
                    _logger.LogInformation($"Prospect with id: {prospect.ProspectId} already has a seller assigned");
                    return (false, "Prospect already has a seller assigned");
                }

                // Tilldela säljaren till det specifika prospektet
                _logger.LogInformation($"Assigning seller to prospect id: {prospect.ProspectId}");
                prospect.Seller = employee;
                prospect.AssignedAgentNumber = employee.AgentNumber;

                await _unitOfWork.Prospects.UpdateAsync(prospect);

                _logger.LogInformation("Saving changes to database");
                await _unitOfWork.SaveChangesAsync();

                return (true, "Seller assigned to the specific prospect");

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error assigning seller to the specific prospect");
                return (false, "Error assigning seller to the specific prospect");
            }
        }

        public async Task<(bool Success, string Message, List<Prospect> prospects)> GetAllCurrentProspects()
        {
            _logger.LogInformation("Getting all current prospects");

            try
            {
                var prospects = await _unitOfWork.Prospects.GetAllProspects();
                _logger.LogInformation($"Found {prospects.Count} current prospects");

                return (true, $"{prospects.Count} current prospects found", prospects);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting all current prospects");
                return (false, "Error getting all current prospects", null);
            }
        }
    }
}

