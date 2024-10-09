﻿using Microsoft.Extensions.Logging;
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
    public class CompanyCustomerService : ICompanyCustomerService
    {
        private ILogger<CompanyCustomerService> _logger;
        private readonly UnitOfWork _unitOfWork;

        public CompanyCustomerService(ILogger<CompanyCustomerService> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<(bool Success, string Message, CompanyCustomer Customer)> GenerateTestCompanyCustomer()
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

        //Create a new company customer
        public async Task<(bool Success, string Message)> CreateCompanyCustomer(CompanyCustomer newCompanyCustomer)
        {
            _logger.LogInformation("Creating new company customer...");

            try
            {
                _logger.LogInformation("Attempting to create a new company customer...");

                // Lägg till det nya företagskunden i databasen
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

        //Update an existing company customer
        public async Task<(bool Success, string Message)> UpdateCompanyCustomer(CompanyCustomer companyCustomer)
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

        public async Task<(bool Success, string Message)> DeleteCompanyCustomer(CompanyCustomer companyCustomer)
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
    }
}


                
            
                

            
        
      

