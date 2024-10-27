using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Models.Employees;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    /// <summary>
    /// This class is responsible for handling the business logic for logging in.
    /// Used to authenticate users in the intial view (login page).
    /// </summary>
    public class LoginService : ILoginService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<LoginService> _logger;

        public LoginService(UnitOfWork unitOfWork, ILogger<LoginService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Method to authenticate a user based on the provided username and password.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<(bool success, string message, Employee employee)> Authentication(string userName, string password)
        {
            _logger.LogInformation("Authenticating user");
            try
            {
                _logger.LogInformation("Checking user credentials");
                var user = await _unitOfWork.Employees.GetEmployeeByUserCredentials(userName, password); 

                _logger.LogInformation("User credentials checked");
                if (user != null)
                {
                    _logger.LogInformation("Login successful");
                    return (true, "Login successful! Welcome back.", user);
                }
                else
                {
                    _logger.LogWarning("Login failed");
                    return (false, "Login failed: No user found with the provided username and password.", null);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while authenticating user");
                return (false, e.Message, null);
            }
        }
    }
}
