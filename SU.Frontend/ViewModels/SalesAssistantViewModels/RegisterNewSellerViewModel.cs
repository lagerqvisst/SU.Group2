using SU.Backend.Controllers;
using SU.Backend.Models.Employees;
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
        private readonly EmployeeController _employeeController;

        public ICommand SaveSellerCommand { get; }

        public RegisterNewSellerViewModel(EmployeeController employeeController)
        {
            _employeeController = employeeController;

            SaveSellerCommand = new RelayCommand(SaveSeller);

        }

        private Employee _selectedSeller;
        public Employee SelectedSeller
        {
            get => _selectedSeller;
            set
            {
                _selectedSeller = value;
                OnPropertyChanged();
            }
        }

        private Employee _selectedSellerType;
        public Employee SelectedSellerType
        {
            get => _selectedSellerType;
            set
            {
                _selectedSellerType = value;
                OnPropertyChanged();
            }
        }

        private async void SaveSeller()
        {
            if (SelectedSeller != null)
            {
                var result = await _employeeController.CreateEmployee(SelectedSeller);

                if (result.success)
                {
                    MessageBox.Show($"{result.message}" + MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show($"{result.message}" + MessageBoxButton.OK);
                }
            }
        }
    }

}
