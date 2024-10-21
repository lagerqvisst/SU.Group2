using SU.Backend.Models.Employees;
using SU.Frontend.Helper.Navigation;
using SU.Frontend.Helper.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Frontend.Helper.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly INavigationService _navigationService;
        private readonly ILoggedInUserService _loggedInUserService;

        public AuthenticationService(INavigationService navigationService, ILoggedInUserService loggedInUserService)
        {
            _navigationService = navigationService;
            _loggedInUserService = loggedInUserService;
        }

        public void Logout()
        {
            // Set loggedInEmployee to null
            _loggedInUserService.LoggedInEmployee = null;

            // Shut down all windows except LoginWindow so new user can log in
            _navigationService.CloseAllExcept("LoginWindow");
        }

    }
}
