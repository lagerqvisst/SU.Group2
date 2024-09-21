using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurance;
using SU.Backend.Models.Insurance.Coverage;
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

        /// <summary>
        /// Exempelflöde
        /// </summary>


        //Steg 1: Skapa försäkringstagare (exempelvis en privatkund)
        public async Task<(bool Success, string Message)> CreateInsurancePolicyHolder(PrivateCustomer? privateCustomer, CompanyCustomer? companyCustomer)
        {
            _logger.LogInformation("Creating insurance policy holder...");

            try
            {
                // If we did not input a company customer, we assume it is a private customer
                if (companyCustomer == null)
                {
                    _logger.LogInformation("Creating insurance policy holder for private customer");

                    _unitOfWork.InsurancePolicyHolders.Add(new InsurancePolicyHolder
                    {
                        PrivateCustomer = privateCustomer
                    });
                }
                else
                {
                    _logger.LogInformation("Creating insurance policy holder for company customer");

                    _unitOfWork.InsurancePolicyHolders.Add(new InsurancePolicyHolder
                    {
                        CompanyCustomer = companyCustomer
                    });

                }

                // Save the changes asynchronously
                _logger.LogInformation("Saving changes to database");
                _unitOfWork.SaveChanges();
                _logger.LogInformation("Changes saved successfully");

                // Return success message after saving
                return (true, "Insurance policy holder created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating insurance policy holder");

                // Return failure message on error
                return (false, "An error occurred while creating the insurance policy holder.");
            }
        }

        //Steg 2 (privatkund): Väljer Privatförsäkringstyp
        //Skapa InsuranceCoverage objekt först. 

        public async Task<(bool Success, string Message)> CreateInsuranceCoverage(Insurance insurance)
        {
            _logger.LogInformation("Creating insurance coverage...");

            try
            {
                var insuranceCoverage = new InsuranceCoverage
                {
                    Insurance = insurance
                };

                _logger.LogInformation("Adding insurance coverage to database");
                _unitOfWork.InsuranceCoverages.Add(insuranceCoverage);
                _logger.LogInformation("Insurance coverage added successfully");
                _unitOfWork.SaveChanges();
                return (true, "Insurance coverage created successfully.");


            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while creating insurance coverage");
                return (false, "An error occurred while creating the insurance coverage.");

            }


        }
        public Task<(bool Success, string Message)> CreateInsuredPerson(string name, string personalNumber)
        {
            _logger.LogInformation("Creating insured person...");

            try
            {
                _unitOfWork.InsuredPersons.Add(new InsuredPerson
                {
                    Name = name,
                    PersonalNumber = personalNumber
                });

                // Save the changes asynchronously
                _logger.LogInformation("Saving changes to database");
                _unitOfWork.SaveChanges();
                _logger.LogInformation("Changes saved successfully");

                // Return success message after saving
                return Task.FromResult((true, "Insured person created successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating insured person");

                // Return failure message on error
                return Task.FromResult((false, "An error occurred while creating the insured person."));
            }
        }

        public async Task<(bool Success, string Message)> CreatePrivateInsuranceCoverage(InsuranceCoverage insuranceCoverage, PrivateCoverageOption privateCoverageOption, InsuredPerson insuredPerson)
        {
            _logger.LogInformation("Creating private insurance coverage...");

            try
            {
                var PrivateInsuranceCoverage = new PrivateCoverage
                {
                    InsuranceCoverage = insuranceCoverage,
                    PrivateCoverageOption = privateCoverageOption,
                    InsuredPerson = insuredPerson
                };

                _logger.LogInformation("Adding private insurance coverage to database");
                _unitOfWork.PrivateCoverages.Add(PrivateInsuranceCoverage);
                _unitOfWork.SaveChanges();
                _logger.LogInformation("Private insurance coverage added successfully");
                return (true, "Private insurance coverage created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating private insurance coverage");

                // Return failure message on error
                return (false, "An error occurred while creating the private insurance coverage.");
            }
        }
    }
}
