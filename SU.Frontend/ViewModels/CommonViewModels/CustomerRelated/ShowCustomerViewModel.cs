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

        // ObservableCollections för kunder
        public ObservableCollection<PrivateCustomer> PrivateCustomers { get; set; } = new ObservableCollection<PrivateCustomer>();
        public ObservableCollection<CompanyCustomer> CompanyCustomers { get; set; } = new ObservableCollection<CompanyCustomer>();

        // Valda kunder
        private PrivateCustomer _selectedPrivateCustomer;
        public PrivateCustomer SelectedPrivateCustomer
        {
            get => _selectedPrivateCustomer;
            set
            {
                _selectedPrivateCustomer = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedPrivateCustomerAsCollection)); // Uppdatera DataGrid

                // När en privatkund väljs, döljer vi företagskunden och visar privatkunden
                if (_selectedPrivateCustomer != null)
                {
                    SelectedCompanyCustomer = null; // Rensa företagskunder
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
                OnPropertyChanged(nameof(SelectedCompanyCustomerAsCollection)); // Uppdatera DataGrid

                // När en företagskund väljs, döljer vi privatkunden och visar företagskunden
                if (_selectedCompanyCustomer != null)
                {
                    SelectedPrivateCustomer = null; // Rensa privatkunder
                    IsPrivateCustomerVisible = false;
                    IsCompanyCustomerVisible = true;
                }
            }
        }

        // Visibilitetsindikatorer för DataGrid
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

        public ShowCustomerViewModel(PrivateCustomerController privateCustomerController, CompanyCustomerController companyCustomerController)
        {
            _privateCustomerController = privateCustomerController;
            _companyCustomerController = companyCustomerController;

            // Ladda kunder asynkront
            LoadCustomersAsync();
        }

        // Ladda båda kundtyperna asynkront
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

        // Egenskaper som omvandlar valda kunder till samlingar så att DataGrid kan visa dem
        public IEnumerable<PrivateCustomer> SelectedPrivateCustomerAsCollection
        {
            get
            {
                if (SelectedPrivateCustomer != null)
                    return new List<PrivateCustomer> { SelectedPrivateCustomer };
                return null;
            }
        }

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


