using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums.Insurance;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Frontend.ViewModels.CommonViewModels.CustomerRelated
{
    public class ShowCustomerViewModel : ObservableObject
    {
        private readonly PrivateCustomerController _privateCustomerController;
        private readonly CompanyCustomerController _companyCustomerController;

        // Byt från List till ObservableCollection
        public ObservableCollection<PrivateCustomer> PrivateCustomers { get; set; } = new ObservableCollection<PrivateCustomer>();
        public ObservableCollection<CompanyCustomer> CompanyCustomers { get; set; } = new ObservableCollection<CompanyCustomer>();

        private PrivateCustomer _selectedPrivateCustomer;
        public PrivateCustomer SelectedPrivateCustomer
        {
            get => _selectedPrivateCustomer;
            set
            {
                _selectedPrivateCustomer = value;
                OnPropertyChanged();
            }
        }

        private CompanyCustomer _selectedCompanyCustomer;
        public CompanyCustomer SelectedCompanyCustomer
        {
            get => _selectedCompanyCustomer;
            set
            {
                _selectedCompanyCustomer = value;
                OnPropertyChanged();
            }
        }

        public ShowCustomerViewModel(PrivateCustomerController privateCustomerController, CompanyCustomerController companyCustomerController)
        {
            _privateCustomerController = privateCustomerController;
            _companyCustomerController = companyCustomerController;

            // Kör asynkrona laddningsmetoder
            LoadCustomersAsync();
        }

        private async Task LoadCustomersAsync()
        {
            await LoadPrivateCustomersAsync();
            await LoadCompanyCustomersAsync();
        }

        private async Task LoadPrivateCustomersAsync()
        {
            // Hämta privatkunder asynkront och lägg till dem i ObservableCollection
            var privateCustomerResult = await _privateCustomerController.GetAllPrivateCustomers();
            PrivateCustomers.Clear();
            foreach (var customer in privateCustomerResult.privateCustomers)
            {
                PrivateCustomers.Add(customer);
            }
        }

        private async Task LoadCompanyCustomersAsync()
        {
            // Hämta företagskunder asynkront och lägg till dem i ObservableCollection
            var companyCustomerResult = await _companyCustomerController.GetAllCompanyCustomers();
            CompanyCustomers.Clear();
            foreach (var customer in companyCustomerResult.companyCustomers)
            {
                CompanyCustomers.Add(customer);
            }
        }

    }

}
