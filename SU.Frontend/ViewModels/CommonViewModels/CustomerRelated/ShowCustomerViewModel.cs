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
        // Controllers for customers
        private readonly PrivateCustomerController _privateCustomerController;
        private readonly CompanyCustomerController _companyCustomerController;

        // ObservableCollections for customers
        public ObservableCollection<PrivateCustomer> PrivateCustomers { get; set; } = new ObservableCollection<PrivateCustomer>();
        public ObservableCollection<CompanyCustomer> CompanyCustomers { get; set; } = new ObservableCollection<CompanyCustomer>();

        // Chosen customers
        private PrivateCustomer _selectedPrivateCustomer;
        public PrivateCustomer SelectedPrivateCustomer
        {
            get => _selectedPrivateCustomer;
            set
            {
                _selectedPrivateCustomer = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedPrivateCustomerAsCollection)); // Update DataGrid

                // When a private customer is selected, hide the company customer and show the private customer
                if (_selectedPrivateCustomer != null)
                {
                    SelectedCompanyCustomer = null; // Clear company customers
                    IsPrivateCustomerVisible = true;
                    IsCompanyCustomerVisible = false;
                }
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
                OnPropertyChanged(nameof(SelectedCompanyCustomerAsCollection)); // Update DataGrid

                // When a company customer is selected, hide the private customer and show the company customer
                if (_selectedCompanyCustomer != null)
                {
                    SelectedPrivateCustomer = null; // Clear private customers
                    IsPrivateCustomerVisible = false;
                    IsCompanyCustomerVisible = true;
                }
            }
        }

        // Visibility properties for the DataGrids
        private bool _isPrivateCustomerVisible;
        public bool IsPrivateCustomerVisible
        {
            get => _isPrivateCustomerVisible;
            set
            {
                _isPrivateCustomerVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isCompanyCustomerVisible;
        public bool IsCompanyCustomerVisible
        {
            get => _isCompanyCustomerVisible;
            set
            {
                _isCompanyCustomerVisible = value;
                OnPropertyChanged();
            }
        }

        // Constructor
        public ShowCustomerViewModel(PrivateCustomerController privateCustomerController, CompanyCustomerController companyCustomerController)
        {
            _privateCustomerController = privateCustomerController;
            _companyCustomerController = companyCustomerController;

            // Ladda kunder asynkront
            LoadCustomersAsync();
        }

        // Load both customer types async
        private async Task LoadCustomersAsync()
        {
            await LoadPrivateCustomersAsync();
            await LoadCompanyCustomersAsync();
        }

        private async Task LoadPrivateCustomersAsync()
        {
            // Get private customers async and add them to ObservableCollection
            var privateCustomerResult = await _privateCustomerController.GetAllPrivateCustomers();
            PrivateCustomers.Clear();
            foreach (var customer in privateCustomerResult.privateCustomers)
            {
                PrivateCustomers.Add(customer);
            }
        }

        private async Task LoadCompanyCustomersAsync()
        {
            // Get company customers async and add them to ObservableCollection
            var companyCustomerResult = await _companyCustomerController.GetAllCompanyCustomers();
            CompanyCustomers.Clear();
            foreach (var customer in companyCustomerResult.companyCustomers)
            {
                CompanyCustomers.Add(customer);
            }
        }

        // Properties to return the selected private customer as a collection
        public IEnumerable<PrivateCustomer> SelectedPrivateCustomerAsCollection
        {
            get
            {
                if (SelectedPrivateCustomer != null)
                    return new List<PrivateCustomer> { SelectedPrivateCustomer };
                return null;
            }
        }

        // Properties to return the selected company customer as a collection
        public IEnumerable<CompanyCustomer> SelectedCompanyCustomerAsCollection
        {
            get
            {
                if (SelectedCompanyCustomer != null)
                    return new List<CompanyCustomer> { SelectedCompanyCustomer };
                return null;
            }
        }
    }
}