using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
using SU.Backend.Models.Insurances;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.SalesAssistantViewModels
{
    public class EditDeleteSellerViewModel : ObservableObject
    {
        private readonly EmployeeController _employeeController;

        public ICommand SaveSellerCommand { get; }
        public ICommand DeleteSellerCommand { get; }

        private ObservableCollection<Employee> _sellers;
        public ObservableCollection<Employee> Sellers
        {
            get => _sellers;
            set
            {
                _sellers = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedSellerAsCollection)); // Update DataGrid
            }
        }

        public List<EmployeeType> SellerRoles { get; } = new List<EmployeeType>
        {
            EmployeeType.InsideSales,
            EmployeeType.OutsideSales
        };

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

        private Employee _selectedSeller;
        public Employee SelectedSeller
        {
            get => _selectedSeller;
            set
            {
                _selectedSeller = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedSellerAsCollection)); // Update DataGrid
            }
        }

        public IEnumerable<Employee> SelectedSellerAsCollection
        {
            get
            {
                if (SelectedSeller != null)
                    return new List<Employee> { SelectedSeller };
                return null;
            }
        }

        // Constructor  for viewmodel
        public EditDeleteSellerViewModel(EmployeeController employeeController)
        {
            _employeeController = employeeController;

            SaveSellerCommand = new RelayCommand(SaveSeller);
            DeleteSellerCommand = new RelayCommand(DeleteSeller);
        }

        private async Task LoadSellersAsync()
        {
            var result = await _employeeController.GetAllSellers();
            if (result.success && result.salesEmployees != null)
            {
                Sellers.Clear();
                foreach (var seller in result.salesEmployees)
                {
                    Sellers.Add(seller);
                }
            }
            else
            {
                Console.WriteLine("Failed to load sellers: " + result.message);
            }
        }


        private async Task<IEnumerable<Employee>> GetSellersAsync()
        {
            // Hämta alla anställda asynkront
            var result = await _employeeController.GetAllSellers();

            if (result.success && result.salesEmployees != null)
            {
                return result.salesEmployees;
            }
            else
            {
                Console.WriteLine("Failed to load sellers: " + result.message);
                return new List<Employee>();
            }
        }

        private async void DeleteSeller()
        {
            var confirm = MessageBox.Show("Are you sure you want to delete this seller?",
                      "Confirm Delete",
                      MessageBoxButton.YesNo);

            if (confirm != MessageBoxResult.Yes) return;

            // Fortsätt med att ta bort om användaren bekräftar
            if (SelectedSeller != null)
            {
                await _employeeController.DeleteEmployee(SelectedSeller);
                Sellers.Remove(SelectedSeller);
                SelectedSeller = null;
            }
        }

        private async void SaveSeller()
        {
            if (SelectedSeller != null)
            {
                var result = await _employeeController.UpdateEmployee(SelectedSeller);

                if (result.success)
                {
                    MessageBox.Show($"{result.message}, MessageBoxButton.OK");
                }
                else
                {
                    MessageBox.Show($"{result.message}, MessageBoxButton.OK");
                }
            }
        }
    }
}
