using System.Windows;
using System.Windows.Input;
using SU.Frontend.Helper;
using SU.Frontend.Helper.Authentication;

namespace SU.Frontend.ViewModels.UserControlViewModels;

public class LogoutButtonViewModel
{
    private readonly IAuthenticationService _authenticationService;

    // Constructor
    public LogoutButtonViewModel(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
        LogoutCommand = new RelayCommand(ConfirmAndLogout);
    }

    // Command
    public ICommand LogoutCommand { get; }

    // Method to prompt user for confirmation before logging out
    private void ConfirmAndLogout()
    {
        // Show a confirmation dialog
        var result = MessageBox.Show(
            "Are you sure you want to log out?",
            "Confirm Logout",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
        );

        // If user confirms, perform logout
        if (result == MessageBoxResult.Yes) _authenticationService.Logout();
    }
}