using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Models.Customers;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    /// <summary>
    /// This class is responsible for handling all the business logic for the CompanyCustomer model.
    /// </summary>
    public class CompanyCustomerService : ICompanyCustomerService
    {
        private ILogger<CompanyCustomerService> _logger;
        private readonly UnitOfWork _unitOfWork;

        public CompanyCustomerService(ILogger<CompanyCustomerService> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        // Method to generate a test company customer, only used for testing purposes
        public async Task<(bool success, string message, CompanyCustomer customer)> GenerateTestCompanyCustomer()
        {
            _logger.LogInformation("Generating test company customer");

            try
            {
                CompanyCustomer companyCustomer = new CompanyCustomer
                {
                    OrganizationNumber = "556677-8890", // Exempel på ett organisationsnummer
                    CompanyName = "Exempel AB",
                    ContactPerson = "Anna Andersson",
                    ContactPersonPhonenumber = "070-1234567",
                    CompanyAdress = "Storgatan 1, 12345 Exempelstad",
                    CompanyPhoneNumber = "08-123456",
                    CompanyLandlineNumber = "08-7654321",
                    CompanyEmailAdress = "info@exempel.se",
                };
                _logger.LogInformation("Company customer generated");

                _logger.LogInformation("Adding company customer to database");

                _unitOfWork.CompanyCustomers.Add(companyCustomer);
                _unitOfWork.SaveChanges();

                _logger.LogInformation("Company customer added to database");

                return (true, "Successfully generated and saved new company customer", companyCustomer);
            }
            catch (Exception)
            {

                return (false, "Failed to generate random company customer", null);
            }
        }

        // Method to create a new company customer
        public async Task<(bool success, string message)> CreateCompanyCustomer(CompanyCustomer newCompanyCustomer)
        {
            _logger.LogInformation("Creating new company customer...");

            try
            {
                _logger.LogInformation("Attempting to create a new company customer...");

                // Add the new company customer to the database
                await _unitOfWork.CompanyCustomers.AddAsync(newCompanyCustomer);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Company customer created successfully.");

                return (true, "Company customer was added to the database.");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString());
                return (false, $"An error occurred while creating the new company customer: {ex.Message}");
            }
        }

        // Method to update an existing company customer
        public async Task<(bool success, string message)> UpdateCompanyCustomer(CompanyCustomer companyCustomer)
        {
            _logger.LogInformation("Updating company customer...");

            try
            {
                _logger.LogInformation("Attempting to update a company customer...");

                // Update company customer in the database
                await _unitOfWork.CompanyCustomers.UpdateAsync(companyCustomer);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Company customer updated successfully.");

                return (true, "Company customer was updated in the database.");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString());
                return (false, $"An error occurred while updating new company customer: {ex.Message}");
            }
        }

        // Method to delete an existing company customer
        public async Task<(bool success, string message)> DeleteCompanyCustomer(CompanyCustomer companyCustomer)
        {

            _logger.LogInformation("Deleting company customer...");

            try
            {
                _logger.LogInformation("Attempting to delete a company customer...");

                // delete company customer in the database
                await _unitOfWork.CompanyCustomers.RemoveAsync(companyCustomer);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Company customer was successfully deleted.");

                return (true, "Company customer was deleted the database.");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString());
                return (false, $"An error occurred while deleting the company customer: {ex.Message}");
            }

        }

        // Method to get a company customer by ID
        public async Task<(bool Success, string Message, CompanyCustomer? Customer)> GetCompanyCustomerById(int id)
        {
            try
            {
                var customer = await _unitOfWork.CompanyCustomers.GetCompanyCustomerById(id);
                return (true, "Successfully retrieved company customer", customer);
            }
            catch (Exception ex)
            {
                return (false, "An error occurred: " + ex.Message, null);
            }
        }

        // Method to get all company customers
        public async Task<(bool Success, string Message, List<CompanyCustomer> CompanyCustomers)> GetAllCompanyCustomers()
        {
            _logger.LogInformation("Controller activated to get all company customers...");

            try
            {
                var companycustomers = _unitOfWork.CompanyCustomers.GetAllCompanyCustomers();
                _logger.LogInformation("Company customers found: {CompanyCustomersCount}", companycustomers.Result.Count);

                return (true, "Company customers found.", companycustomers.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching company customers.");
                return (false, "An error occurred while fetching the company customers.", new List<CompanyCustomer>());
            }
        }
    }
}


                
            
                

            
        
      

