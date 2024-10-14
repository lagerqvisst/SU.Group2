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
    public class InsuranceService : IInsuranceService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<InsuranceService> _logger;

        public InsuranceService(UnitOfWork unitOfWork, ILogger<InsuranceService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<(bool Success, string Message)> CreatePrivateInsurance(
            PrivateCustomer privateCustomer,
            InsuranceType insuranceType,
            PrivateCoverageOption privateCoverageOption,
            Employee seller,
            bool isPolicyHolderInsured,
            InsuredPerson? insuredPerson = null) // Nullable insuredPerson om det är en annan person
        {
            _logger.LogInformation("Creating private insurance...");

            try
            {
                // Skapa huvudobjektet - Insurance
                var insurance = new Insurance
                {
                    InsuranceType = insuranceType,
                    InsuranceStatus = InsuranceStatus.Requested,  // Sätter status till Requested som default
                    PaymentPlan = PaymentPlan.Monthly,
                    StartDate = DateTime.Now,  // Sätter startdatum till nuvarande tidpunkt. //Kan ändras som input parameter
                    EndDate = DateTime.Now.AddYears(1),
                    Note = "This is a test insurance" //Addera som input parameter?
                };

                if (privateCustomer == null)
                {
                    return (false, "No private customer provided.");
                }
                _logger.LogInformation("Customer provided: {CustomerId} - {CustomerName}", privateCustomer.PrivateCustomerId, privateCustomer.FirstName);

                // Skapa InsurancePolicyHolder och koppla till försäkringen
                insurance.InsurancePolicyHolder = new InsurancePolicyHolder
                {
                    PrivateCustomer = privateCustomer
                };

                // Kontrollera om coverage option är null
                if (privateCoverageOption == null)
                {
                    return (false, "No private coverage option provided.");
                }

                _logger.LogInformation("Calculating premium based on coverage option.");
                insurance.Premium = privateCoverageOption.MonthlyPremium;  // Grundpremien

                _logger.LogInformation("Total premium calculated: {CalculatedPremium}", insurance.Premium);

                // Skapa PrivateCoverage
                var insuranceCoverage = new InsuranceCoverage();
                var privateCoverage = new PrivateCoverage
                {
                    InsuranceCoverage = insuranceCoverage,
                    PrivateCoverageOption = privateCoverageOption,
                    InsuredPerson = isPolicyHolderInsured
                        ? new InsuredPerson // Om försäkringstagaren är den försäkrade
                        {
                            Name = $"{privateCustomer.FirstName} {privateCustomer.LastName}",
                            PersonalNumber = privateCustomer.PersonalNumber
                        }
                        : insuredPerson // Om försäkringstagaren inte är den försäkrade
                };

                if (privateCoverage.InsuredPerson == null)
                {
                    return (false, "No insured person provided.");
                }

                // Koppla navigationsobjekten
                insuranceCoverage.PrivateCoverage = privateCoverage;
                insurance.InsuranceCoverage = insuranceCoverage;

                // Lägg till vem som sålde försäkringen
                if (seller != null)
                {
                    insurance.Seller = seller;
                    _logger.LogInformation("Seller added to insurance: {SellerId} - {SellerName}", seller.EmployeeId, seller.FirstName);
                }
                else
                {
                    _logger.LogWarning("No seller found.");
                    return (false, "No seller found.");
                }

                // Lägg till försäkring till databasen
                await _unitOfWork.Insurances.AddAsync(insurance);
                await _unitOfWork.SaveChangesAsync();

                return (true, "Insurance created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the insurance");
                return (false, "An error occurred while creating the insurance.");
            }
        }



        //Create a company insurance
        public async Task<(bool Success, string Message)> CreateTestCompanyInsurance()
        {
            _logger.LogInformation("Starting the process of creating a company insurance...");

            try
            {
                // Create the insurance object
                var insurance = new Insurance
                {
                    InsuranceType = InsuranceType.VehicleInsurance,
                    InsuranceStatus = InsuranceStatus.Active,
                    PaymentPlan = PaymentPlan.Monthly,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(1),
                    Note = "This is a test insurance"
                };
                _logger.LogInformation("Insurance object created successfully.");

                // Fetch the last company customer
                _logger.LogInformation("Attempting to fetch a test company customer...");
                var companyCustomer = _unitOfWork.CompanyCustomers.GetCompanyCustomers().Result.Last();
                if (companyCustomer == null)
                {
                    _logger.LogWarning("No company customer found.");
                    return (false, "No company customer found.");
                }
                _logger.LogInformation("Company customer found: {Org Nr} - {CompanyName}", companyCustomer.OrganizationNumber, companyCustomer.CompanyName);

                // Create InsurancePolicyHolder
                _logger.LogInformation("Assigning the company customer as the insurance policy holder...");
                insurance.InsurancePolicyHolder = new InsurancePolicyHolder
                {
                    CompanyCustomer = companyCustomer
                };
                _logger.LogInformation("InsurancePolicyHolder created and assigned successfully.");

                // Fetch the last risk zone
                _logger.LogInformation("Attempting to fetch a risk zone...");
                var riskZone = _unitOfWork.Riskzones.GetRiskZones().Result.Last();
                if (riskZone == null)
                {
                    _logger.LogWarning("No risk zone found.");
                    return (false, "No risk zone found.");
                }
                _logger.LogInformation("Risk zone found: {RiskzoneId} - {RiskzoneName}", riskZone.RiskzoneId, riskZone.RiskzoneLevel.ToString());

                // Fetch the last vehicle insurance option
                _logger.LogInformation("Attempting to fetch a vehicle insurance option...");
                var vehicleInsuranceOption = _unitOfWork.VehicleInsuranceOptions.GetVehicleInsuranceOptions().Result.Last();
                if (vehicleInsuranceOption == null)
                {
                    _logger.LogWarning("No vehicle insurance option found.");
                    return (false, "No vehicle insurance option found.");
                }
                _logger.LogInformation("Vehicle insurance option found: {OptionId} - {OptionDescription}", vehicleInsuranceOption.VehicleInsuranceOptionId, vehicleInsuranceOption.OptionDescription.ToString());

                // Create VehicleInsuranceCoverage
                _logger.LogInformation("Creating vehicle insurance coverage...");
                var insuranceCoverage = new InsuranceCoverage();
                var vehicleCoverage = new VehicleInsuranceCoverage
                {
                    InsuranceCoverage = insuranceCoverage,
                    VehicleInsuranceOption = vehicleInsuranceOption,
                    Riskzone = riskZone,
                    CoverageAmount = vehicleInsuranceOption.OptionCost,
                    MonthlyPremium = PremiumCalculator.GetVehicleInsurancePremium(riskZone, vehicleInsuranceOption)
                };
                _logger.LogInformation("Vehicle insurance coverage created successfully with monthly premium: {MonthlyPremium}", vehicleCoverage.MonthlyPremium);

                insuranceCoverage.VehicleInsuranceCoverage = vehicleCoverage;
                insurance.Premium = vehicleCoverage.MonthlyPremium; //move premium from vehicle to insurance
                insurance.InsuranceCoverage = insuranceCoverage;

                // Fetch the seller with a specified role
                _logger.LogInformation("Attempting to fetch a seller with the role: InsideSales...");
                var seller = await _unitOfWork.Employees.GetEmployeeByRole(EmployeeType.InsideSales);
                if (seller == null)
                {
                    _logger.LogWarning("No seller found with the role: InsideSales.");
                }
                else
                {
                    insurance.Seller = seller;
                    _logger.LogInformation("Seller assigned to insurance: {SellerId} - {SellerName}", seller.EmployeeId, seller.FirstName);
                }

                // Save changes to the database
                _logger.LogInformation("Saving the new company insurance to the database...");
                await _unitOfWork.Insurances.AddAsync(insurance);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Company insurance created and saved successfully.");

                return (true, "Company insurance created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating company insurance.");
                return (false, "An error occurred while creating the company insurance.");
            }
        }

        public async Task<(bool Success, string Message)> CreateTestCompanyInsuranceProperty()
        {
            _logger.LogInformation("Starting the process of creating a company insurance...");

            try
            {
                // Create the insurance object
                var insurance = new Insurance
                {
                    InsuranceType = InsuranceType.PropertyAndInventoryInsurance,
                    InsuranceStatus = InsuranceStatus.Active,
                    PaymentPlan = PaymentPlan.Monthly,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(1),
                    Note = "This is a test insurance"
                };
                _logger.LogInformation("Insurance object created successfully.");

                // Fetch the last company customer
                _logger.LogInformation("Attempting to fetch a test company customer...");
                var companyCustomer = _unitOfWork.CompanyCustomers.GetCompanyCustomers().Result.Last();
                if (companyCustomer == null)
                {
                    _logger.LogWarning("No company customer found.");
                    return (false, "No company customer found.");
                }
                _logger.LogInformation("Company customer found: {Org Nr} - {CompanyName}", companyCustomer.OrganizationNumber, companyCustomer.CompanyName);

                // Create InsurancePolicyHolder
                _logger.LogInformation("Assigning the company customer as the insurance policy holder...");
                insurance.InsurancePolicyHolder = new InsurancePolicyHolder
                {
                    CompanyCustomer = companyCustomer
                };
                _logger.LogInformation("InsurancePolicyHolder created and assigned successfully.");

                
                // Create VehicleInsuranceCoverage
                _logger.LogInformation("Creating vehicle insurance coverage...");
                var insuranceCoverage = new InsuranceCoverage();
                var propertyCoverage = new PropertyAndInventoryCoverage
                {
                    InsuranceCoverage = insuranceCoverage,
                    PropertyAddress = "Testgatan 1",
                    PropertyValue = 1_000_000,
                    PropertyPremium = 1000,
                    InventoryValue = 500_000,
                    InventoryPremium = 500,
                };

                insuranceCoverage.PropertyAndInventoryCoverage = propertyCoverage;
                insurance.Premium = 1000; //egentligen räknas ut med logik från bilagan.
                insurance.InsuranceCoverage = insuranceCoverage;

                // Fetch the seller with a specified role
                _logger.LogInformation("Attempting to fetch a seller with the role: InsideSales...");
                var seller = await _unitOfWork.Employees.GetEmployeeByRole(EmployeeType.InsideSales);
                if (seller == null)
                {
                    _logger.LogWarning("No seller found with the role: InsideSales.");
                }
                else
                {
                    insurance.Seller = seller;
                    _logger.LogInformation("Seller assigned to insurance: {SellerId} - {SellerName}", seller.EmployeeId, seller.FirstName);
                }

                // Save changes to the database
                _logger.LogInformation("Saving the new company insurance to the database...");
                await _unitOfWork.Insurances.AddAsync(insurance);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Company insurance created and saved successfully.");

                return (true, "Company insurance created successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating insurance");
                return (false, "An error occurred while creating the insurance.");
            }
        }

        public async Task<(bool Success, string Message)> CreateTestCompanyLiability()
        {
            _logger.LogInformation("Starting the process of creating a company insurance...");

            try
            {
                // Create the insurance object
                var insurance = new Insurance
                {
                    InsuranceType = InsuranceType.LiabilityInsurance,
                    InsuranceStatus = InsuranceStatus.Active,
                    PaymentPlan = PaymentPlan.Monthly,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(1),
                    Note = "This is a test insurance"
                };
                _logger.LogInformation("Insurance object created successfully.");

                // Fetch the last company customer
                _logger.LogInformation("Attempting to fetch a test company customer...");
                var companyCustomer = _unitOfWork.CompanyCustomers.GetCompanyCustomers().Result.Last();
                if (companyCustomer == null)
                {
                    _logger.LogWarning("No company customer found.");
                    return (false, "No company customer found.");
                }
                _logger.LogInformation("Company customer found: {Org Nr} - {CompanyName}", companyCustomer.OrganizationNumber, companyCustomer.CompanyName);

                // Create InsurancePolicyHolder
                _logger.LogInformation("Assigning the company customer as the insurance policy holder...");
                insurance.InsurancePolicyHolder = new InsurancePolicyHolder
                {
                    CompanyCustomer = companyCustomer
                };
                _logger.LogInformation("InsurancePolicyHolder created and assigned successfully.");


                _logger.LogInformation("Creating liability insurance coverage option...");
                var liabiltiyCoverageOption = _unitOfWork.LiabilityCoverageOptions.GetLiabilityCoverage().Result.First();
                if(liabiltiyCoverageOption == null)
                {
                    return (false, "No liability coverage option found.");
                }

                var insuranceCoverage = new InsuranceCoverage();
                var liabilityCoverage = new LiabilityCoverage
                {
                    InsuranceCoverage = insuranceCoverage,
                    LiabilityCoverageOption = liabiltiyCoverageOption,
                };

                insuranceCoverage.LiabilityCoverage = liabilityCoverage;
                insurance.Premium = liabiltiyCoverageOption.MonthlyPremium; 
                insurance.InsuranceCoverage = insuranceCoverage;

                // Fetch the seller with a specified role
                _logger.LogInformation("Attempting to fetch a seller with the role: InsideSales...");
                var seller = await _unitOfWork.Employees.GetEmployeeByRole(EmployeeType.InsideSales);
                if (seller == null)
                {
                    _logger.LogWarning("No seller found with the role: InsideSales.");
                }
                else
                {
                    insurance.Seller = seller;
                    _logger.LogInformation("Seller assigned to insurance: {SellerId} - {SellerName}", seller.EmployeeId, seller.FirstName);
                }

                // Save changes to the database
                _logger.LogInformation("Saving the new company insurance to the database...");
                await _unitOfWork.Insurances.AddAsync(insurance);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Company insurance created and saved successfully.");

                return (true, "Company insurance created successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating insurance");
                return (false, "An error occurred while creating the insurance.");
            }
        }

        public async Task<(bool Success, string Message)> CreateTestPrivateInsurance()
        {

            // test hakuna matata tarea
            await Console.Out.WriteLineAsync("hola");

            _logger.LogInformation("Creating private insurance...");


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
                    Note = "This is a test insurance",
                  
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

                if(insurance.InsurancePolicyHolder.PrivateCustomer == null)
                {
                    return (false, "The insurance policyholder object did not link to a customer");
                }

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
                insurance.InsuranceCoverage = insuranceCoverage;
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
