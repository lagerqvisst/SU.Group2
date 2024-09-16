using SU.Backend.Database;
using SU.Backend.Helper;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employee;
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
        private readonly IRandomGenerationService _randomInfoGenerationService;
        private readonly UnitOfWork _unitOfWork; 

        public PrivateCustomerService(IRandomGenerationService randomInfoGenerationService, UnitOfWork unitOfWork)
        {
            _randomInfoGenerationService = randomInfoGenerationService;
            _unitOfWork = unitOfWork;
        }
        public async Task<(bool Success, string Message, PrivateCustomer Customer)> GenerateRandomPrivateCustomer()
        {
            try
            {
                var (success, randomUser) = _randomInfoGenerationService.GenerateRandomCustomer().Result;

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
    }
}
