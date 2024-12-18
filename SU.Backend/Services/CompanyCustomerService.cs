﻿using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Models.Customers;
using SU.Backend.Services.Interfaces;

namespace SU.Backend.Services;

/// <summary>
///     This class is responsible for handling all the business logic for the CompanyCustomer model.
/// </summary>
public class CompanyCustomerService : ICompanyCustomerService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly ILogger<CompanyCustomerService> _logger;

    public CompanyCustomerService(ILogger<CompanyCustomerService> logger, UnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
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
    public async Task<(bool success, string message, CompanyCustomer? customer)> GetCompanyCustomerById(int id)
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
    public async Task<(bool success, string message, List<CompanyCustomer> companyCustomers)> GetAllCompanyCustomers()
    {
        _logger.LogInformation("Controller activated to get all company customers...");

        try
        {
            var companycustomers = await _unitOfWork.CompanyCustomers.GetAllCompanyCustomers();
            _logger.LogInformation("Company customers found: {CompanyCustomersCount}", companycustomers.Count);

            return (true, "Company customers found.", companycustomers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching company customers.");
            return (false, "An error occurred while fetching the company customers.", new List<CompanyCustomer>());
        }
    }
}