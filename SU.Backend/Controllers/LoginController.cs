using Microsoft.Extensions.Logging;
using SU.Backend.Models.Employees;
using SU.Backend.Services;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Controllers
{
    /// <summary>
    /// This class is responsible for handling the login controller.
    /// Makes logic available in the Viewmodel
    /// More info about the logic for each method can be found in the Service function each controller method uses.
    /// </summary>
    public class LoginController
    {
        // Services
        private readonly ILoginService _loginService;
        private readonly ILogger<LoginController> _logger;

        // Constructor
        public LoginController(ILoginService loginService, ILogger<LoginController> logger)
        {
            _loginService = loginService;
            _logger = logger;
        }

        // Controller for Authentication method
        public async Task<(bool success, string message, Employee employee)> Authentication(string userName, string password)
        {
            _logger.LogInformation("User input collected... starting authentication process.");
            _logger.LogInformation($"Sending input [Username: {userName}, Password: {password}] to LoginService...");

            var result = await _loginService.Authentication(userName, password); // Antag att LoginResult returneras här

            if (result.success)
            {
                _logger.LogInformation($"Login successful for : {result.employee.FirstName} {result.employee.LastName}");
            }
            else
            {
                _logger.LogWarning($"Login failed: {result.message}");
            }

            return result;
        }
    }
}
