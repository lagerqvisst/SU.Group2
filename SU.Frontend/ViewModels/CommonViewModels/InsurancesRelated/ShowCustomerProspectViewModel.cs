using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Enums.Prospects;
using SU.Backend.Models.Insurances.Prospects;
using SU.Backend.Models;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SU.Backend.Models.Employees;
using SU.Backend.Services.Interfaces;

namespace SU.Frontend.ViewModels.CommonViewModels.InsurancesRelated
{
    public class ShowCustomerProspectViewModel : ObservableObject
    {
        // Controllers
        private readonly ProspectController _prospectController;
        private readonly EmployeeController _employeeController;

        // Constructor
        public ShowCustomerProspectViewModel(ProspectController prospectController, EmployeeController employeeController)
        {
            _prospectController = prospectController;
            _employeeController = employeeController;

            Prospects = new ObservableCollection<Prospect>();
            Sellers = new ObservableCollection<Employee>();

            OnInitialized();

            ExportToExcel = new RelayCommand(ExportProspectsToExcel);
        }

        // List of all prospects
        private ObservableCollection<Prospect> _prospects;
        public ObservableCollection<Prospect> Prospects
        {
            get => _prospects;
            set
            {
                _prospects = value;
                OnPropertyChanged();
            }
        }

        // List of sellers to choose from
        public ObservableCollection<Employee> Sellers { get; set; }




        // List of available ProspectStatus options
        public List<ProspectStatus> ProspectStatusOptions => Enum.GetValues(typeof(ProspectStatus)).Cast<ProspectStatus>().ToList();



        // Commands
        public ICommand SaveProspectCommand { get; }
        public ICommand ExportToExcel { get; }

        private async Task LoadProspectsAsync()
        {
            await LoadAllProspectsAsync();
        }

        // Method to load all prospects from the controller
        private async Task LoadAllProspectsAsync()
        {
            var prospectResult = await _prospectController.IdentifyNewProspects();
            if (prospectResult.prospects?.Any() ?? false)
            {
                Prospects.Clear();
                foreach (var prospect in prospectResult.prospects)
                {
                    Prospects.Add(prospect);
                }
            }
            else
            {
                Console.WriteLine("No prospects found");
            }
        }

        // Method to load all sellers from the controller
        private async Task LoadSellersAsync()
        {
            var sellersResult = await _employeeController.GetAllSellers();
            Sellers.Clear();
            foreach (var seller in sellersResult.salesEmployees)
            {
                Sellers.Add(seller);
            }
        }

        private async Task IdentifyNewProspects()
        {
            await _prospectController.IdentifyNewProspects();
        }

        // Method to save the selected prospect to the database


        // Method to initialize the ViewModel
        private async Task OnInitialized()
        {
            await IdentifyNewProspects();
            await LoadSellersAsync();
            await LoadProspectsAsync();
        }




        private async void ReloadProspects()
        {
            await LoadProspectsAsync();
        }

        // Method to export the prospects to an Excel file
        private async void ExportProspectsToExcel()
        {
            var prospectList = Prospects.ToList();

            var result = await _prospectController.ExportProspectsToExcel(prospectList);

            if(result.success)
            {
                MessageBox.Show($"{result.message}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"{result.message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
