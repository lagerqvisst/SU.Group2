using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Enums.Prospects;
using SU.Backend.Models.Insurances.Prospects;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Frontend.ViewModels.CommonViewModels.InsurancesRelated
{
    public class ShowCustomerProspectViewModel : ObservableObject
    {
        private readonly PrivateCustomerController _privateCustomerController;
        private readonly CompanyCustomerController _companyCustomerController;

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
                OnPropertyChanged(nameof(SelectedPrivateCustomerAsCollection));

                if (_selectedPrivateCustomer != null)
                {
                    SelectedCompanyCustomer = null;
                    IsPrivateCustomerVisible = true;
                    IsCompanyCustomerVisible = false;
                }
                else
                {
                    IsPrivateCustomerVisible = false; // Om ingen privatkund är vald, göm panelen.
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
                OnPropertyChanged(nameof(SelectedCompanyCustomerAsCollection));

                if (_selectedCompanyCustomer != null)
                {
                    SelectedPrivateCustomer = null;
                    IsPrivateCustomerVisible = false;
                    IsCompanyCustomerVisible = true;
                }
                else
                {
                    IsCompanyCustomerVisible = false; // Om ingen företagskund är vald, göm panelen.
                }
            }
        }

        // Visibility flags
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

        public ShowCustomerProspectViewModel(PrivateCustomerController privateCustomerController, CompanyCustomerController companyCustomerController)
        {
            _privateCustomerController = privateCustomerController;
            _companyCustomerController = companyCustomerController;

            // Initialt gömma båda panelerna
            IsPrivateCustomerVisible = false;
            IsCompanyCustomerVisible = false;

            LoadCustomersAsync();
        }

        private async Task LoadCustomersAsync()
        {
            await LoadPrivateCustomersAsync();
            await LoadCompanyCustomersAsync();
        }

        private async Task LoadPrivateCustomersAsync()
        {
            var privateCustomerResult = await _privateCustomerController.GetAllPrivateCustomers();
            PrivateCustomers.Clear();
            foreach (var customer in privateCustomerResult.privateCustomers)
            {
                PrivateCustomers.Add(customer);
            }
        }

        private async Task LoadCompanyCustomersAsync()
        {
            var companyCustomerResult = await _companyCustomerController.GetAllCompanyCustomers();
            CompanyCustomers.Clear();
            foreach (var customer in companyCustomerResult.companyCustomers)
            {
                CompanyCustomers.Add(customer);
            }
        }

        public IEnumerable<PrivateCustomer> SelectedPrivateCustomerAsCollection =>
            SelectedPrivateCustomer != null ? new List<PrivateCustomer> { SelectedPrivateCustomer } : null;

        public IEnumerable<CompanyCustomer> SelectedCompanyCustomerAsCollection =>
            SelectedCompanyCustomer != null ? new List<CompanyCustomer> { SelectedCompanyCustomer } : null;
    }





}




