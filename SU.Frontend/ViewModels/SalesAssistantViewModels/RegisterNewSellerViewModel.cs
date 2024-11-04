using SU.Backend.Controllers;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
using SU.Backend.Models.Insurances;
using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.SalesAssistantViewModels
{
    public class RegisterNewSellerViewModel : ObservableObject
    {
        // Controller
        private readonly EmployeeController _employeeController;

        // Command
        public ICommand SaveSellerCommand { get; }

        public List<EmployeeType> SellerRoles { get; } = new List<EmployeeType>
        {
            EmployeeType.InsideSales,
            EmployeeType.OutsideSales
        };

        #region Properties

        private EmployeeType _selectedRole;
        public EmployeeType SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged();
            }
        }

        private string _personalNumber;
        public string PersonalNumber
        {
            get => _personalNumber;
            set
            {
                _personalNumber = value;
                OnPropertyChanged();
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        private EmployeeType _selectedSellerType;
        public EmployeeType SelectedSellerType
        {
            get => _selectedSellerType;
            set
            {
                _selectedSellerType = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        // Constructor
        public RegisterNewSellerViewModel(EmployeeController employeeController)
        {
            _employeeController = employeeController;

            SaveSellerCommand = new RelayCommand(SaveSeller);

        }

        // Method to save a new seller
        private async void SaveSeller()
        {
            var result = await _employeeController.CreateEmployee(SelectedRole, FirstName, LastName, PersonalNumber);

            if (result.success)
            {
                // Skapa en sammanfattningstext med information om den nya anställda
                var roleAssignments = string.Join(", ", result.employee.RoleAssignments.Select(r => r.Role.ToString()));

                var summary = $"Successfully created employee:\n\n" +
                              $"First Name: {result.employee.FirstName}\n" +
                              $"Last Name: {result.employee.LastName}\n" +
                              $"Username: {result.employee.UserName}\n" +
                              $"Password: {result.employee.Password}\n" +
                              $"Email: {result.employee.Email}\n" +
                              $"Manager: {(result.employee.Manager != null ? $"{result.employee.Manager.FirstName} {result.employee.Manager.LastName} (ID: {result.employee.ManagerId})" : "N/A")}\n" +
                              $"Agent Number: {result.employee.AgentNumber ?? "N/A"}\n" +
                              $"Base Salary: {result.employee.BaseSalary:C}\n" +
                              $"Roles: {roleAssignments}";

                MessageBox.Show(summary, "Employee Created", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(result.message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Check if seller can be saved
        private bool CanSaveSeller()
        {
            return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(PersonalNumber);
        }
    }
}