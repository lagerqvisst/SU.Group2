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
        private readonly ILoginService _loginService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILoginService loginService, ILogger<LoginController> logger)
        {
            _loginService = loginService;
            _logger = logger;

        }

        public async Task<(bool Success, string Message, Employee Employee)> Authentication(string Username, string Password)
        {
            _logger.LogInformation("User input collected... starting authentication process.");
            _logger.LogInformation($"Sending input [Username: {Username}, Password: {Password}] to LoginService...");

            var result = await _loginService.Authentication(Username, Password); // Antag att LoginResult returneras här

            if (result.Success)
            {
                _logger.LogInformation($"Login successful for : {result.Employee.FirstName} {result.Employee.LastName}");
            }
            else
            {
                _logger.LogWarning($"Login failed: {result.Message}");
            }

            return result; // Returnera resultatet
        }

    }
}
