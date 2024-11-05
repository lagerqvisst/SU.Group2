using System.Windows;
using System.Windows.Input;
using SU.Backend.Controllers;
using SU.Backend.Helper;
using SU.Backend.Models.Enums;
using SU.Frontend.Helper;
using SU.Frontend.Helper.Authentication;

namespace SU.Frontend.ViewModels.UserControlViewModels;

public class TaskbarViewModel : ObservableObject
{
    // Service
    private readonly IAuthenticationService _authenticationService;
    private readonly EmployeeController _employeeController;

    // Controllers
    private readonly LoginController _loginController;
    private readonly LoginViewModel _loginViewModel;

    private EmployeeType _selectedEmployeeType;

    // Constructor
    public TaskbarViewModel(
        IAuthenticationService authenticationService,
        LoginController loginController,
        EmployeeController employeeController,
        LoginViewModel loginViewModel)
    {
        _authenticationService = authenticationService;
        _loginController = loginController;
        _employeeController = employeeController;
        _loginViewModel = loginViewModel;

        FetchDemoCredentialsCommand = new RelayCommand(OnFetchDemoCredentials);
        ExitApplicationCommand = new RelayCommand(ConfirmAndExitApplication);
        LogOutCommand = new RelayCommand(ConfirmAndLogout);
    }

    // Commands
    public ICommand ExitApplicationCommand { get; }
    public ICommand LogOutCommand { get; }
    public ICommand FetchDemoCredentialsCommand { get; }

    // Properties
    public List<EmployeeType> EmployeeTypes { get; set; } = EnumService.EmployeeType();

    public EmployeeType SelectedEmployeeType
    {
        get => _selectedEmployeeType;
        set
        {
            _selectedEmployeeType = value;
            OnPropertyChanged();
        }
    }

    // Method to confirm and exit application
    private void ConfirmAndExitApplication()
    {
        var result = MessageBox.Show(
            "Are you sure you want to exit the application?",
            "Confirm Exit",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes) Application.Current.Shutdown();
    }

    // Method to confirm and logout
    private void ConfirmAndLogout()
    {
        var result = MessageBox.Show(
            "Are you sure you want to log out?",
            "Confirm Logout",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes) _authenticationService.Logout();
    }

    // Method to fetch demo credentials
    private async void OnFetchDemoCredentials()
    {
        if (SelectedEmployeeType != null)
            try
            {
                var employeeInfo = await _employeeController.GetEmployeeByRole(SelectedEmployeeType);
                if (employeeInfo.success)
                {
                    _loginViewModel.UserName = employeeInfo.employee.UserName;
                    _loginViewModel.Password = employeeInfo.employee.Password;
                }
                else
                {
                    MessageBox.Show($"{employeeInfo.message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
    }
}