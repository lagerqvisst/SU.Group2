using SU.Backend.Controllers;
using SU.Backend.Helper;
using SU.Backend.Models.Enums;
using SU.Frontend.Helper;
using SU.Frontend.Helper.Authentication;
using System.Windows;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.UserControlViewModels
{
    public class TaskbarViewModel : ObservableObject
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly LoginController _loginController;
        private readonly EmployeeController _employeeController;
        private readonly LoginViewModel _loginViewModel;

        public ICommand ExitApplicationCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand FetchDemoCredentialsCommand { get; set; }

        public TaskbarViewModel(IAuthenticationService authenticationService, LoginController loginController, EmployeeController employeeController, LoginViewModel loginViewModel)
        {

            _loginController = loginController;
            _employeeController = employeeController;
            _loginViewModel = loginViewModel;

            FetchDemoCredentialsCommand = new RelayCommand(OnFetchDemoCredentials);
            ExitApplicationCommand = new RelayCommand(() => Application.Current.Shutdown());
            LogOutCommand = new RelayCommand(authenticationService.Logout);


        }

        public List<EmployeeType> EmployeeTypes { get; set; } = EnumService.EmployeeType();
        private EmployeeType _selectedEmployeeType;
        public EmployeeType SelectedEmployeeType
        {
            get => _selectedEmployeeType;
            set
            {
                _selectedEmployeeType = value;
                OnPropertyChanged();
            }
        }


        private async void OnFetchDemoCredentials()
        {
            if (SelectedEmployeeType != null)
            {
                try
                {
                    var employeeInfo = await _employeeController.GetEmployeeByRole(SelectedEmployeeType);
                    if (employeeInfo.success)
                    {
                        // Updates the viewmodel with the fetched credentials
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
                    MessageBox.Show($"Ett oväntat fel inträffade: {ex.Message}");
                }
            }
        }




    }


}
