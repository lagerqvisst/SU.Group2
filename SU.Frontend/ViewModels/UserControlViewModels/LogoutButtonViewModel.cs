using SU.Frontend.Helper.Authentication;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace SU.Frontend.ViewModels.UserControlViewModels
{
    public class LogoutButtonViewModel
    {
        // Command
        public ICommand LogoutCommand { get; }

        private readonly IAuthenticationService _authenticationService;

        // Constructor
        public LogoutButtonViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            LogoutCommand = new RelayCommand(ConfirmAndLogout);
        }

        // Method to prompt user for confirmation before logging out
        private void ConfirmAndLogout()
        {
            // Show a confirmation dialog
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Logout",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            // If user confirms, perform logout
            if (result == MessageBoxResult.Yes)
            {
                _authenticationService.Logout();
            }
        }
    }
}
