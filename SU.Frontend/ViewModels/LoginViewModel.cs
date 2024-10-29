﻿using SU.Backend.Controllers;
using SU.Backend.Helper;
using SU.Backend.Models.Enums;
using SU.Frontend.Helper;
using SU.Frontend.Helper.DI_Objects.User;
using SU.Frontend.Helper.Navigation;
using System.Diagnostics;
using System.Windows;

namespace SU.Frontend.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private readonly LoginController _loginController;
        private readonly EmployeeController _employeeController;
        private readonly INavigationService _navigationService;
        private readonly ILoggedInUserService _loggedInUserService;


        public LoginViewModel(LoginController loginController,  INavigationService navigationService, ILoggedInUserService loggedInUserService, EmployeeController employeeController)
        {
            _loginController = loginController;
            _navigationService = navigationService;
            _loggedInUserService = loggedInUserService;

            LoginCommand = new RelayCommand(OnLogin, CanLogin);
            // replaced by taskbar FetchEmployeeCommand = new RelayCommand(OnFetchEmployee, CanFetchEmployee); // Nytt kommando

            _userName = string.Empty;
            _password = string.Empty;
            ButtonContent = "Logga in";
            _employeeController = employeeController;
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
                IsLoading = true;
                await Task.Delay(100); // Small delay to allow UI update

                var result = await _loginController.Authentication(UserName, Password);
                if (result.success)
                {
                    _loggedInUserService.LoggedInEmployee = result.employee;

                    _navigationService.NavigateToMainViewBasedOnRole(result.employee);
                }

                //MessageBox.Show($"{result.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett oväntat fel inträffade: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }




    }
}

