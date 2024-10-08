using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Helper;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    public class PrivateCustomerService : IPrivateCustomerService
    {
        private ILogger<PrivateCustomerService> _logger; 
        private readonly IRandomGenerationService _randomInfoGenerationService;
        private readonly UnitOfWork _unitOfWork; 

        public PrivateCustomerService(IRandomGenerationService randomInfoGenerationService, UnitOfWork unitOfWork)
        {
            _randomInfoGenerationService = randomInfoGenerationService;
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool Success, string Message, /*måste den här vara här??=>*/PrivateCustomer Customer)> DeletePrivateCustomer(PrivateCustomer PrivateCustomer)
        {
            _logger.LogInformation("Deleting private customer...");

            try
            {
                _logger.LogInformation("Attempting to delete a private customer...");

                await _unitOfWork.PrivateCustomers.RemoveAsync(PrivateCustomer);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Private customer was successfully deleted.");

                return (true, "Private customer was deleted the database.", PrivateCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString());
                return (false, $"An error occurred while deleting the private customer: {ex.Message}", null);
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
    }
}
