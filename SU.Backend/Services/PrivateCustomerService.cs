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
    public class PrivateCustomerService : IPrivateCustomerService
    {
        private ILogger<PrivateCustomerService> _logger; 
        private readonly IRandomGenerationService _randomInfoGenerationService;
        private readonly UnitOfWork _unitOfWork; 

        public PrivateCustomerService(IRandomGenerationService randomInfoGenerationService, UnitOfWork unitOfWork, ILogger<PrivateCustomerService> logger)
        {
            _logger = logger;
            _randomInfoGenerationService = randomInfoGenerationService;
            _unitOfWork = unitOfWork;
        }
        // method to update a private customer 
        public async Task<(bool Success, string Message)> UpdatePrivateCustomer(PrivateCustomer privateCustomer)
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
        public async Task<(bool Success, string Message)> DeletePrivateCustomer(PrivateCustomer privateCustomer)
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
        public async Task<(bool Success, string Message)> CreateNewPrivateCustomer(PrivateCustomer privateCustomer)
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

        public async Task<(bool Success, string Message, PrivateCustomer Customer)> GenerateRandomPrivateCustomer()
        {
            try
            {
                var (success, randomUser) = _randomInfoGenerationService.GenerateSingleRandomUser().Result;

                if (success)
                {
                    var info = randomUser.Results[0];

                    var customer = new PrivateCustomer
                    {
                        PersonalNumber = PrivateCustomerHelper.GenerateCompletePersonalNumber(info),
                        FirstName = info.Name.First,
                        LastName = info.Name.Last,
                        Email = info.Email,
                        PhoneNumber = info.Phone,
                        Address = $"{info.Location.Street.Number} {info.Location.Street.Name} {info.Location.Postcode}",

                    }; 

                    _unitOfWork.PrivateCustomers.Add(customer);
                    _unitOfWork.SaveChanges();

                    return (true, "Successfully generated and saved new employee", customer);

                }
                else
                {
                    return (false, "Failed to generate random employee", null);
                }
            }
            catch (Exception ex)
            {

                return (false, "An error occurred: " + ex.Message, null);

            }
        }

        public async Task<(bool Success, string Message, PrivateCustomer Customer)> GetPrivateCustomerById(PrivateCustomer privateCustomer)
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

        public async Task<(bool Success, string Message, List<PrivateCustomer>)> GetPrivateCustomers()
        {
            try
            {
                var customers = await _unitOfWork.PrivateCustomers.GetPrivateCustomers();
                return (true, "Successfully retrieved customers", customers);

            }
            catch (Exception ex)
            {

                return (false, "An error occurred: " + ex.Message, null);
            }
        }

        public async Task<(bool Success, string Message, List<PrivateCustomer> PrivateCustomers)> ListAllPrivateCustomers()
        {
            _logger.LogInformation("Controller activated to get all private customers...");

            try
            {
                var privatecustomers = _unitOfWork.PrivateCustomers.GetPrivateCustomers();
                _logger.LogInformation("Private customers found: {PrivateCustomersCount}", privatecustomers.Result.Count);

                return (true, "Private customers found.", privatecustomers.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching private customers.");
                return (false, "An error occurred while fetching the private customers.", new List<PrivateCustomer>());
            }
        }
    }
}

