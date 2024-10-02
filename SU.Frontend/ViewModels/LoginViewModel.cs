using SU.Backend.Controllers;
using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;
using SU.Frontend.Helper.User;
using System.Diagnostics;
using System.Windows;

namespace SU.Frontend.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private readonly LoginController _loginController;
        private readonly INavigationService _navigationService;
        private readonly ILoggedInUserService _loggedInUserService;


        public LoginViewModel(LoginController loginController, INavigationService navigationService, ILoggedInUserService loggedInUserService)
        {
            _loginController = loginController;
            _navigationService = navigationService;
            _loggedInUserService = loggedInUserService;

            LoginCommand = new RelayCommand(OnLogin, CanLogin);
            _userName = string.Empty;
            _password = string.Empty;
            ButtonContent = "Logga in";
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
                // Update ButtonContent based on loading state
                ButtonContent = _isLoading ? "Laddar..." : "Logga in";
            }
        }

        private string _buttonContent;
        public string ButtonContent
        {
            get => _buttonContent;
            set
            {
                _buttonContent = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand LoginCommand { get; }

        private bool CanLogin() => !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);

        private async void OnLogin()
        {
            try
            {
                // Set loading state before starting authentication
                // TODO: Ensure text changes before async operation.
                IsLoading = true;

                await Task.Delay(100); // Small delay to allow UI update


                var result = await _loginController.Authentication(UserName, Password);
                if (result.Success)
                {
                    //Vi kan använda denna användare i olika views utan att behöva skicka den vidare.
                    _loggedInUserService.LoggedInEmployee = result.Employee;
                    //(Employee)
                    //TODO: Antingen göra overload så att vi kan skicka med ett objekt eller bara en vy. För vi kan kalla på loggeduser i nästa vy utan att passera den.
                    //_navigationService.NavigateTo("NEXT VIEW", "OBJECT");
                }
                MessageBox.Show($"{result.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett oväntat fel inträffade: {ex.Message}");
            }
            finally
            {
                // Reset loading state after authentication is complete
                IsLoading = false;
            }
        }
    }
}

