using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.CommonViewModels.CustomerRelated
{
    public class EditDeleteCustomerViewModel : ObservableObject
    {
        private readonly PrivateCustomerController _privateCustomerController;
        private readonly CompanyCustomerController _companyCustomerController;

        public ICommand SaveCustomerCommand { get; }
        public ICommand DeleteCustomerCommand { get; }

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

        public EditDeleteCustomerViewModel(PrivateCustomerController privateCustomerController, CompanyCustomerController companyCustomerController)
        {
            _privateCustomerController = privateCustomerController;
            _companyCustomerController = companyCustomerController;

            // Ladda kunder asynkront
            LoadCustomersAsync();

            SaveCustomerCommand = new RelayCommand(SaveCustomer);
            DeleteCustomerCommand = new RelayCommand(DeleteCustomer, CanDeleteCustomer);
        }

        private async void SaveCustomer()
        {
            var confirm = MessageBox.Show("Customer has been updated",
                      "Confirm", MessageBoxButton.OK);

            if (SelectedPrivateCustomer != null)
            {
                await _privateCustomerController.UpdatePrivateCustomer(SelectedPrivateCustomer);
            }
            else if (SelectedCompanyCustomer != null)
            {
                await _companyCustomerController.UpdateCompanyCustomer(SelectedCompanyCustomer);
            }
        }

        private bool CanDeleteCustomer()
        {
            return SelectedPrivateCustomer != null || SelectedCompanyCustomer != null;
        }

        private async void DeleteCustomer()
        {
            var confirm = MessageBox.Show("Are you sure you want to delete this customer?",
                                  "Confirm Delete",
                                  MessageBoxButton.YesNo);

            if (confirm != MessageBoxResult.Yes) return;

            // Fortsätt med att ta bort om användaren bekräftar
            if (SelectedPrivateCustomer != null)
            {
                await _privateCustomerController.DeletePrivateCustomer(SelectedPrivateCustomer);
                PrivateCustomers.Remove(SelectedPrivateCustomer);
                SelectedPrivateCustomer = null;
            }
            else if (SelectedCompanyCustomer != null)
            {
                await _companyCustomerController.DeleteCompanyCustomer(SelectedCompanyCustomer);
                CompanyCustomers.Remove(SelectedCompanyCustomer);
                SelectedCompanyCustomer = null;
            }

            IsPrivateCustomerVisible = false;
            IsCompanyCustomerVisible = false;
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