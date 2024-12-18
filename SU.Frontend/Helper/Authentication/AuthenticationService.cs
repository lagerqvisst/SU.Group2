﻿using SU.Frontend.Helper.DI_Objects.User;
using SU.Frontend.Helper.Navigation;

namespace SU.Frontend.Helper.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly ILoggedInUserService _loggedInUserService;

    // Services needed for the AuthenticationService
    private readonly INavigationService _navigationService;

    // Constructor
    public AuthenticationService(INavigationService navigationService, ILoggedInUserService loggedInUserService)
    {
        _navigationService = navigationService;
        _loggedInUserService = loggedInUserService;
    }

    // Method to log out the user
    public void Logout()
    {
        // Set loggedInEmployee to null
        _loggedInUserService.LoggedInEmployee = null;

        // Stäng alla fönster utom login-fönstret vid utloggning
        _navigationService.CloseAllExcept("LoginWindow", minimizeLoginWindow: false);
    }

}