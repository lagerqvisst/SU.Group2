using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Helper;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    /// <summary>
    /// This class is responsible for handling all the business logic for the PrivateCustomer model.
    /// </summary>
    public class PrivateCustomerService : IPrivateCustomerService
    {
        private ILogger<PrivateCustomerService> _logger; 
        
        private readonly UnitOfWork _unitOfWork; 

        public PrivateCustomerService(UnitOfWork unitOfWork, ILogger<PrivateCustomerService> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        // method to update a private customer 
        public async Task<(bool success, string message)> UpdatePrivateCustomer(PrivateCustomer privateCustomer)
        {
            _logger.LogInformation("Updating private customer...");

            try
            {
                _logger.LogInformation("Attempting to update a private customer...");

                await _unitOfWork.PrivateCustomers.UpdateAsync(privateCustomer);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Private customer has been successfully updated.");

                return (true, "The private customer has been updated on the database.");
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.ToString());
                return (false, $"An error has occurred while updating the private customer: {e.Message.ToString()}");
            }
        }

        //method to delete an existing private customer
        public async Task<(bool success, string message)> DeletePrivateCustomer(PrivateCustomer privateCustomer)
        {
            _logger.LogInformation("Deleting private customer...");

            try
            {
                _logger.LogInformation("Attempting to delete a private customer...");

                await _unitOfWork.PrivateCustomers.RemoveAsync(privateCustomer);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Private customer was successfully deleted.");

                return (true, "Private customer was deleted the database.");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString());
                return (false, $"An error occurred while deleting the private customer: {ex.Message.ToString()}");

            }
        }
        
        // method to create a new private customer
        public async Task<(bool success, string message)> CreateNewPrivateCustomer(PrivateCustomer privateCustomer)
        {
            _logger.LogInformation("Creating new Private Customer...");

            try
            {
                _logger.LogInformation("Attemptig to save to database...");
                await _unitOfWork.PrivateCustomers.AddAsync(privateCustomer);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("New private customer has been succesfully added to the database");

                return (true, "The new private customer was succesfully added to the system.");
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.ToString());
                return (false, $"There was an error saving the new user: {e.Message.ToString()}");
            }

        }

        // method to get a private customer by id
        public async Task<(bool success, string message, PrivateCustomer customer)> GetPrivateCustomerById(PrivateCustomer privateCustomer)
        {
            try
            {
                var customer = await _unitOfWork.PrivateCustomers.GetPrivateCustomerById(privateCustomer);
                return (true, "Successfully retrieved customer", customer);
            }
            catch (Exception ex)
            {
                return (false, "An error occurred: " + ex.Message, null);
            }
        }

        // method to get all private customers
        public async Task<(bool success, string message, List<PrivateCustomer> privateCustomers)> GetAllPrivateCustomers()
        {
            _logger.LogInformation("Controller activated to get all private customers...");

            try
            {
                var privatecustomers =  await _unitOfWork.PrivateCustomers.GetAllPrivateCustomers();
                _logger.LogInformation("Private customers found: {PrivateCustomersCount}", privatecustomers.Count);

                return (true, "Private customers found.", privatecustomers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching private customers.");
                return (false, "An error occurred while fetching the private customers.", new List<PrivateCustomer>());
            }
        }
    }
}

