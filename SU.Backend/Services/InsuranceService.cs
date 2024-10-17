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
            string? note,
            DateTime? startDate = null,
            DateTime? endDate = null,
            List<InsuranceAddonType>? addons = null, // Nullable lista med addons
            InsuredPerson? insuredPerson = null)
        {
            _logger.LogInformation("Creating private insurance...");

            try
            {
                // Validera indata
                if (privateCustomer == null) return (false, "No private customer provided.");
                if (privateCoverageOption == null) return (false, "No private coverage option provided.");
                if (!isPolicyHolderInsured && (insuredPerson == null ||
                    string.IsNullOrEmpty(insuredPerson.InsuredPersonName) ||
                    string.IsNullOrEmpty(insuredPerson.InsuredPersonPersonalNumber)))
                {
                    return (false, "No insured person provided.");
                }
                if (seller == null) return (false, "No seller found.");

                // Skapa försäkringen
                var insurance = new Insurance
                {
                    InsuranceType = insuranceType,
                    InsuranceStatus = InsuranceStatus.Requested,
                    PaymentPlan = PaymentPlan.Monthly,
                    StartDate = startDate ?? DateTime.Now,
                    EndDate = endDate ?? DateTime.Now.AddYears(1),
                    Note = note,

                    InsurancePolicyHolder = new InsurancePolicyHolder
                    {
                        PrivateCustomer = privateCustomer
                    },

                    InsuranceCoverage = new InsuranceCoverage
                    {
                        PrivateCoverage = new PrivateCoverage
                        {
                            PrivateCoverageOption = privateCoverageOption,

                            // Om försäkringstagaren är försäkrad, använd försäkringstagarens uppgifter, annars ta ny input.
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

                // Använd helper-metoden för att hantera tillägg och uppdatera premie
                InsuranceBuilder.ApplyAddons(insurance, addons);

                // Spara försäkringen i databasen
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

         public async Task<(bool Success, string Message, List<InsuranceAddonType> InsuranceAddonTypes)> GetAllInsuranceAddonTypes()
         {
            _logger.LogInformation("Controller activated to get all insurance addon types...");

            try
            {
                var insuranceAddonTypes = _unitOfWork.InsuranceAddonTypes.GetAllInsuranceAddonTypes();
                _logger.LogInformation("Insurance addon types found: {InsuranceAddonTypeCount}", insuranceAddonTypes.Result.Count);

                return (true, "Insurance addon types found", insuranceAddonTypes.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching insurance addont types.");
                return (false, "An error occurred while fetching the insurance addon types.", new List<InsuranceAddonType>());
            }
         }

        public async Task<(bool Success, string Message)> CreatePropertyInventoryInsurance(
            CompanyCustomer companyCustomer,
            PropertyAndInventoryCoverage propertyAndInventoryCoverage,
            Employee seller,
            string note,
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
                    PaymentPlan = PaymentPlan.Monthly,
                    StartDate = startDate ?? DateTime.Now,  // Om inget startdatum skickas in används nuvarande tidpunkt
                    EndDate = endDate ?? DateTime.Now.AddYears(1), // Om inget slutdatum skickas in används ett år framåt
                    Note = note ?? "No note provided.", // Sätter default note om ingen skickas in

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

        public async Task<(bool Success, string Message)> CreateLiabilityInsurance(
           CompanyCustomer companyCustomer,
           LiabilityCoverage liabilityCoverage,
           Employee seller,
           string note,
           DateTime? startDate = null,
           DateTime? endDate = null)
        {
            _logger.LogInformation("Starting the process of creating a company liability insurance...");

            try
            {
                // Validera inputdata
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

                // Skapa försäkringsobjektet
                var insurance = new Insurance
                {
                    InsuranceType = InsuranceType.LiabilityInsurance,
                    InsuranceStatus = InsuranceStatus.Requested,
                    PaymentPlan = PaymentPlan.Monthly,
                    StartDate = startDate ?? DateTime.Now,  // Använd inskickat startdatum, annars nuvarande tid
                    EndDate = endDate ?? DateTime.Now.AddYears(1),  // Använd inskickat slutdatum, annars ett år framåt
                    Note = note,

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

                // Spara ändringarna till databasen
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

        #region Tests
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
                var companyCustomer = _unitOfWork.CompanyCustomers.GetAllCompanyCustomers().Result.Last();
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
                var companyCustomer = _unitOfWork.CompanyCustomers.GetAllCompanyCustomers().Result.Last();
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
                /*
                var propertyCoverage = new PropertyAndInventoryCoverage
                {
                    InsuranceCoverage = insuranceCoverage,
                    PropertyAddress = "Testgatan 1",
                    PropertyValue = 1_000_000,
                    PropertyPremium = 1000,
                    InventoryValue = 500_000,
                    InventoryPremium = 500,
                };*/

                //insuranceCoverage.PropertyAndInventoryCoverage = propertyCoverage;
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
                // Fetch the last company customer
                _logger.LogInformation("Attempting to fetch a test company customer...");
                var companyCustomer = _unitOfWork.CompanyCustomers.GetAllCompanyCustomers().Result.Last();
                if (companyCustomer == null)
                {
                    _logger.LogWarning("No company customer found.");
                    return (false, "No company customer found.");
                }
                _logger.LogInformation("Company customer found: {Org Nr} - {CompanyName}", companyCustomer.OrganizationNumber, companyCustomer.CompanyName);

                _logger.LogInformation("Creating liability insurance coverage option...");
                var liabiltiyCoverageOption = _unitOfWork.LiabilityCoverageOptions.GetLiabilityCoverage().Result.First();
                if (liabiltiyCoverageOption == null)
                {
                    return (false, "No liability coverage option found.");
                }


                // Fetch the seller with a specified role
                _logger.LogInformation("Attempting to fetch a seller with the role: InsideSales...");
                var seller = await _unitOfWork.Employees.GetEmployeeById(1);


                // Create the insurance object
                var insurance = new Insurance
                {
                    InsuranceType = InsuranceType.LiabilityInsurance,
                    InsuranceStatus = InsuranceStatus.Active,
                    PaymentPlan = PaymentPlan.Monthly,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(1),
                    Note = "This is a test insurance",

                    InsurancePolicyHolder = new InsurancePolicyHolder
                    {
                        CompanyCustomer = companyCustomer,
                    }, 

                    InsuranceCoverage = new InsuranceCoverage
                    {
                        LiabilityCoverage = new LiabilityCoverage
                        {
                            LiabilityCoverageOption = liabiltiyCoverageOption,
                        }
                    }, 

                    Premium = liabiltiyCoverageOption.MonthlyPremium,
                    Seller = seller
                };
                _logger.LogInformation("Insurance object created successfully.");



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
                var privateCustomer = _unitOfWork.PrivateCustomers.GetAllPrivateCustomers().Result.Last();
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

        public async Task<(bool Success, string Message)> CreateVehicleInsurance(
            CompanyCustomer companyCustomer,
            VehicleInsuranceCoverage vehicleCoverage,
            Employee seller,
            string note,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            _logger.LogInformation("Starting the process of creating a company vehicle insurance...");

            try
            {
                // Validera inputdata
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

                // Skapa försäkringsobjektet
                var insurance = new Insurance
                {
                    InsuranceType = InsuranceType.VehicleInsurance,
                    InsuranceStatus = InsuranceStatus.Active,
                    PaymentPlan = PaymentPlan.Monthly,
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

        public async Task<(bool Success, string Message, List<InsurancePolicyHolder> InsurancePolicyHolders)> ListAllInsurancePolicyHolders()
        {
            _logger.LogInformation("Controller activated to list all insurance policy holders...");

            try
            {
                var insurancePolicyHolders = _unitOfWork.InsurancePolicyHolders.ListAllInsurancePolicyHolders();
                _logger.LogInformation("Insurance policy holders retrieved succesfully: {InsurancePolicyHolderCount}", insurancePolicyHolders.Result.Count);

                return (true , "Insurance policy holders retrieved successfully.", insurancePolicyHolders.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching insurance policy holders.");
                return (false, "An error occurred while fetching the insurance policy holders.", new List<InsurancePolicyHolder>());
            }
        }





        #endregion







    }
}
