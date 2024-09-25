using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Database.Interfaces;
using SU.Backend.Database.Repositories;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Enums;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Enums.Insurance.Addons;
using SU.Backend.Models.Insurances;
using SU.Backend.Models.Insurances.Coverage;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<InsuranceService> _logger;

        public InsuranceService(UnitOfWork unitOfWork, ILogger<InsuranceService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }



        public async Task<(bool Success, string Message)> CreateTestInsurance()
        {
            _logger.LogInformation("Creating insurance...");

            try
            {
                // Skapa huvudobjektet - Insurance
                var insurance = new Insurance
                {
                    InsuranceType = InsuranceType.ChildAccidentAndHealthInsurance,
                    InsuranceStatus = InsuranceStatus.Active,
                    PaymentPlan = PaymentPlan.Monthly,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(1),
                    Note = "This is a test insurance"
                };

                _logger.LogInformation("Finding test customer.");
                // Hämta PrivateCustomer för InsurancePolicyHolder
                var privateCustomer = _unitOfWork.PrivateCustomers.GetPrivateCustomers().Result.Last();
                if (privateCustomer == null)
                {
                    return (false, "No private customer found.");
                }
                _logger.LogInformation("Test customer found: {CustomerId} - {CustomerName}", privateCustomer.PrivateCustomerId, privateCustomer.FirstName);

                //_logger.LogInformation

                _logger.LogInformation("Creating InsurancePolicyHolder");
                // Skapa InsurancePolicyHolder
                insurance.InsurancePolicyHolder = new InsurancePolicyHolder
                {
                    PrivateCustomer = privateCustomer
                };
                _logger.LogInformation("Assigned fetched customer as insurance policy holder of insurance");

                // Skapa InsuranceCoverage
               

                var currentYear = DateTime.Now.Year;
                var privateCoverageOption = await _unitOfWork.PrivateCoverageOptions
                    .GetSpecificPrivateCoverageOption(750000, new DateTime(currentYear, 1, 1), insurance.InsuranceType);

                if (privateCoverageOption == null)
                {
                    return (false, "No private coverage option found for the input.");
                }

                 _logger.LogInformation("Adding addon");
                var addon = await _unitOfWork.InsuranceAddonTypes.GetSpecificAddonType(AddonType.SicknessAccident, 100_000m);
                if (addon == null)
                {
                    return (false, "No addon found for the input.");
                }
                _logger.LogInformation("Addon found");

                insurance.InsuranceAddons.Add(new InsuranceAddon
                {
                    InsuranceAddonType = addon
                });

                _logger.LogInformation("Calculating premium, adding both default premium and addon-premium (if added). Default Premium: {DefaultPremium}, Addon Premium: {AddonPremium}",
                    privateCoverageOption.MonthlyPremium,
                    addon.BaseExtraPremium);

                insurance.Premium = privateCoverageOption.MonthlyPremium + addon.BaseExtraPremium;

                _logger.LogInformation("Premium calculated: {CalculatedPremium}", insurance.Premium);

                // Skapa PrivateCoverage
                var insuranceCoverage = new InsuranceCoverage();
                var privateCoverage = new PrivateCoverage
                {
                    InsuranceCoverage = insuranceCoverage,
                    PrivateCoverageOption = privateCoverageOption,
                    InsuredPerson = new InsuredPerson
                    {
                        Name = "Test Testsson",
                        PersonalNumber = "19900101-1234"
                    }
                };

                // Koppla navigationsobjekten
                insuranceCoverage.PrivateCoverage = privateCoverage;

                // Lägg till vem som sålde försäkringen.
                _logger.LogInformation("Adding seller to insurance");

                // Hämta säljaren med angiven roll.
                var seller = await _unitOfWork.Employees.GetEmployeeByRole(EmployeeType.InsideSales);

                // Logga information om säljaren.
                if (seller != null)
                {
                    insurance.Seller = seller;
                    _logger.LogInformation("Seller added to insurance: {SellerId} - {SellerName}", seller.EmployeeId, seller.FirstName);
                }
                else
                {
                    _logger.LogWarning($"No seller found.");
                }

                // Lägg till Insurance till databasen
                await _unitOfWork.Insurances.AddAsync(insurance);
                await _unitOfWork.SaveChangesAsync();


                return (true, "Insurance created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating insurance");
                return (false, "An error occurred while creating the insurance.");
            }
        }


        public async Task<(bool Success, string Message)> RemoveAllInsurances()
        {
            try
            {
                _logger.LogInformation("Removing all insurances...");
                var insurances = await _unitOfWork.Insurances.GetAllInsurances();

                if (insurances == null || !insurances.Any())
                {
                    _logger.LogInformation("No insurances found to remove.");
                    return (false, "No insurances found to remove.");
                }

                _logger.LogInformation($"Removing {insurances.Count} insurances...");
                
                foreach( var insurance in insurances)
                {

                    await _unitOfWork.Insurances.RemoveAsync(insurance);
                    //Workaround until we fix Cascade Delete problem...
                    await _unitOfWork.InsurancePolicyHolders.RemoveAsync(insurance.InsurancePolicyHolder);
                    await _unitOfWork.InsuredPersons.RemoveAsync(insurance.InsuranceCoverage.PrivateCoverage.InsuredPerson);
                }

                _logger.LogInformation("Saving changes...");
                _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("All insurances removed successfully.");

                return (true, "All insurances removed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing insurances");
                return (false, $"An error occurred while removing insurances: {ex.Message}");
            }
        }
        


    }
}
