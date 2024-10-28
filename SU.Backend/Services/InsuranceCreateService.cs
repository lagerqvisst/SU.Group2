using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Database.Interfaces;
using SU.Backend.Database.Repositories;
using SU.Backend.Helper;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
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
    /// <summary>
    /// This class contains methods for creating different types of insurances.
    /// </summary>
    public class InsuranceCreateService : IInsuranceCreateService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<InsuranceCreateService> _logger;

        public InsuranceCreateService(UnitOfWork unitOfWork, ILogger<InsuranceCreateService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// This method creates a private insurance based on the provided input data.
        /// Many inputs are required to create a private insurance, such as a private customer, insurance type, coverage option, seller, etc.
        /// This is due to the insurance object having a lot of related entities in the database
        /// </summary>

        public async Task<(bool Success, string Message)> CreatePrivateInsurance(
            PrivateCustomer privateCustomer,
            InsuranceType insuranceType,
            PrivateCoverageOption privateCoverageOption,
            Employee seller,
            bool isPolicyHolderInsured,
            string? note,
            PaymentPlan paymentPlan,
            DateTime? startDate = null,
            DateTime? endDate = null,
            List<InsuranceAddonType>? addons = null, // Nullable lista med addons
            InsuredPerson? insuredPerson = null)
        {
            _logger.LogInformation("Creating private insurance...");

            try
            {
                // Validating inputs
                if (privateCustomer == null) return (false, "No private customer provided.");
                if (privateCoverageOption == null) return (false, "No private coverage option provided.");
                if (!isPolicyHolderInsured && (insuredPerson == null ||
                    string.IsNullOrEmpty(insuredPerson.InsuredPersonName) ||
                    string.IsNullOrEmpty(insuredPerson.InsuredPersonPersonalNumber)))
                {
                    return (false, "No insured person provided.");
                }
                if (seller == null) return (false, "No seller found.");
                if(note == null) note = "No note provided.";

                // Creating the insurance object
                var insurance = new Insurance
                {
                    InsuranceType = insuranceType,
                    InsuranceStatus = InsuranceStatus.Requested,
                    PaymentPlan = paymentPlan,
                    StartDate = (startDate ?? DateTime.Today).Date,
                    EndDate = (endDate ?? DateTime.Today.AddYears(1)).Date,
                    Note = note,
                    InsuranceCategory = InsuranceCategory.Private,

                    InsurancePolicyHolder = new InsurancePolicyHolder
                    {
                        PrivateCustomer = privateCustomer
                    },

                    InsuranceCoverage = new InsuranceCoverage
                    {
                        PrivateCoverage = new PrivateCoverage
                        {
                            PrivateCoverageOption = privateCoverageOption,

                            // If the policy holder is the same as insured person, the insured person is the same as the policy holder
                            InsuredPersonName = isPolicyHolderInsured
                                ? $"{privateCustomer.FirstName} {privateCustomer.LastName}"
                                : insuredPerson?.InsuredPersonName,
                            InsuredPersonPersonalNumber = isPolicyHolderInsured
                                ? privateCustomer.PersonalNumber
                                : insuredPerson?.InsuredPersonPersonalNumber
                        }
                    },

                    Premium = privateCoverageOption.MonthlyPremium,

                    Seller = seller
                };

                // Adding addons to the insurance
                InsuranceBuilder.ApplyAddons(insurance, addons);

                // Saving the insurance to the database
                await _unitOfWork.Insurances.AddAsync(insurance);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Private insurance created successfully with total premium: {Premium}", insurance.Premium);
                return (true, "Insurance created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the insurance");
                return (false, "An error occurred while creating the insurance.");
            }
        }

        // This method is similar to private insurance creation, but for a specific company insurances.
        public async Task<(bool Success, string Message)> CreatePropertyInventoryInsurance(
            CompanyCustomer companyCustomer,
            PropertyAndInventoryCoverage propertyAndInventoryCoverage,
            Employee seller,
            string note,
            PaymentPlan paymentPlan,
            DateTime? startDate = null, // Nullable startDate parameter, om man inte anger ett datum när försäkringen ska gälla, tas dagens datum.
            DateTime? endDate = null    // Nullable endDate parameter
)
        {
            _logger.LogInformation("Creating property and inventory insurance...");

            try
            {
                // Kontrollera att nödvändiga parametrar inte är null
                if (companyCustomer == null) return (false, "No company customer provided.");
                if (propertyAndInventoryCoverage == null) return (false, "No property and inventory coverage provided.");
                if (seller == null) return (false, "No seller provided.");

                // Skapa huvudobjektet - Insurance
                var insurance = new Insurance
                {
                    InsuranceType = InsuranceType.PropertyAndInventoryInsurance,
                    InsuranceStatus = InsuranceStatus.Requested,  // Sätter status till Requested som default
                    PaymentPlan = paymentPlan,
                    StartDate = startDate ?? DateTime.Now,  // Om inget startdatum skickas in används nuvarande tidpunkt
                    EndDate = endDate ?? DateTime.Now.AddYears(1), // Om inget slutdatum skickas in används ett år framåt
                    Note = note ?? "No note provided.", // Sätter default note om ingen skickas in
                    InsuranceCategory = InsuranceCategory.Company,

                    InsurancePolicyHolder = new InsurancePolicyHolder
                    {
                        CompanyCustomer = companyCustomer
                    },

                    InsuranceCoverage = new InsuranceCoverage
                    {
                        PropertyAndInventoryCoverage = propertyAndInventoryCoverage
                    },

                    Premium = PremiumCalculator.CalculateTotalPropertyAndInventoryPremium(
                        propertyAndInventoryCoverage?.PropertyPremium ?? 0,
                        propertyAndInventoryCoverage?.InventoryPremium ?? 0),

                    Seller = seller // Säkerställer att seller inte är null
                };

                _logger.LogInformation("Total premium calculated: {CalculatedPremium}", insurance.Premium);
                _logger.LogInformation("Property and inventory insurance created successfully.");
                _logger.LogInformation("Saving the new property and inventory insurance to the database...");

                await _unitOfWork.Insurances.AddAsync(insurance);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Property and inventory insurance created and saved successfully.");

                return (true, "Property and inventory insurance created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the insurance");
                return (false, "An error occurred while creating the insurance.");
            }
        }

        // This method is similar to private insurance creation, but for a specific company insurances.
        public async Task<(bool Success, string Message)> CreateLiabilityInsurance(
           CompanyCustomer companyCustomer,
           LiabilityCoverage liabilityCoverage,
           Employee seller,
           string note,
           PaymentPlan paymentPlan,
           DateTime? startDate = null,
           DateTime? endDate = null)
        {
            _logger.LogInformation("Starting the process of creating a company liability insurance...");

            try
            {
                // Validate input data
                if (companyCustomer == null)
                {
                    _logger.LogWarning("No company customer provided.");
                    return (false, "No company customer provided.");
                }
                _logger.LogInformation("Company customer provided: {Org Nr} - {CompanyName}", companyCustomer.OrganizationNumber, companyCustomer.CompanyName);

                if (liabilityCoverage == null)
                {
                    _logger.LogWarning("No liability coverage option provided.");
                    return (false, "No liability coverage option provided.");
                }

                if (seller == null)
                {
                    _logger.LogWarning("No seller provided.");
                    return (false, "No seller provided.");
                }

                // Create the insurance object
                var insurance = new Insurance
                {
                    InsuranceType = InsuranceType.LiabilityInsurance,
                    InsuranceStatus = InsuranceStatus.Requested,
                    PaymentPlan = paymentPlan,
                    StartDate = startDate ?? DateTime.Now,  // Använd inskickat startdatum, annars nuvarande tid
                    EndDate = endDate ?? DateTime.Now.AddYears(1),  // Använd inskickat slutdatum, annars ett år framåt
                    Note = note,
                    InsuranceCategory = InsuranceCategory.Company,

                    InsurancePolicyHolder = new InsurancePolicyHolder
                    {
                        CompanyCustomer = companyCustomer
                    },

                    InsuranceCoverage = new InsuranceCoverage
                    {
                        LiabilityCoverage = liabilityCoverage
                    },

                    Premium = liabilityCoverage.LiabilityCoverageOption.MonthlyPremium,
                    Seller = seller
                };
                _logger.LogInformation("Insurance object created successfully.");

                // Save changes to the database
                _logger.LogInformation("Saving the new company insurance to the database...");
                await _unitOfWork.Insurances.AddAsync(insurance);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Company insurance created and saved successfully.");

                return (true, "Company liability insurance created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating insurance");
                return (false, "An error occurred while creating the insurance.");
            }
        }

        public async Task<(bool Success, string Message)> CreateVehicleInsurance(
            CompanyCustomer companyCustomer,
            VehicleInsuranceCoverage vehicleCoverage,
            Employee seller,
            string note,
            PaymentPlan paymentPlan,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            _logger.LogInformation("Starting the process of creating a company vehicle insurance...");

            try
            {
                // Validate input data
                if (companyCustomer == null)
                {
                    _logger.LogWarning("No company customer provided.");
                    return (false, "No company customer provided.");
                }
                _logger.LogInformation("Company customer provided: {Org Nr} - {CompanyName}", companyCustomer.OrganizationNumber, companyCustomer.CompanyName);

                if (vehicleCoverage == null)
                {
                    _logger.LogWarning("No vehicle insurance coverage provided.");
                    return (false, "No vehicle insurance coverage provided.");
                }

                if (seller == null)
                {
                    _logger.LogWarning("No seller provided.");
                    return (false, "No seller provided.");
                }

                // Create the insurance object
                var insurance = new Insurance
                {
                    InsuranceType = InsuranceType.VehicleInsurance,
                    InsuranceStatus = InsuranceStatus.Active,
                    PaymentPlan = paymentPlan,
                    StartDate = startDate ?? DateTime.Now,  // Använd inskickat startdatum, annars nuvarande tid
                    EndDate = endDate ?? DateTime.Now.AddYears(1),  // Använd inskickat slutdatum, annars ett år framåt
                    Note = note,

                    InsurancePolicyHolder = new InsurancePolicyHolder
                    {
                        CompanyCustomer = companyCustomer
                    },

                    InsuranceCoverage = new InsuranceCoverage
                    {
                        VehicleInsuranceCoverage = vehicleCoverage
                    },

                    Premium = PremiumCalculator.GetVehicleInsurancePremium(vehicleCoverage.Riskzone, vehicleCoverage.VehicleInsuranceOption),
                    Seller = seller
                };
                _logger.LogInformation("Insurance object created successfully.");

                // Spara ändringarna till databasen
                _logger.LogInformation("Saving the new company vehicle insurance to the database...");
                await _unitOfWork.Insurances.AddAsync(insurance);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Company vehicle insurance created and saved successfully.");

                return (true, "Company vehicle insurance created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating company vehicle insurance.");
                return (false, "An error occurred while creating the company vehicle insurance.");
            }
        }

        // Deletes an insurance from the database
        public async Task<(bool Success, string Message)> DeleteInsurance(Insurance insurance)
        {
            _logger.LogInformation("Deleting insurance...");

            try
            {
                _logger.LogInformation("Attempting to delete an insurance...");

                await _unitOfWork.Insurances.RemoveAsync(insurance);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Insurance was successfully deleted.");

                return (true, "Insurance was deleted the database.");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString());
                return (false, $"An error occurred while deleting the insurance: {ex.Message.ToString()}");
            }
        }

        public async Task<(bool Success, string Message)> UpdateInsurance(Insurance insurance)
        {
            _logger.LogInformation("Updating insurance...");

            try
            {
                await _unitOfWork.Insurances.UpdateAsync(insurance);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Insurance was successfully updated.");

                return (true, "Insurance was updated in the database.");
            }
            catch (Exception ex)
            {

                _logger.LogWarning(ex.ToString());
                return (false, $"An error occurred while updating the insurance: {ex.Message.ToString()}");
            }
        }
    }
}
