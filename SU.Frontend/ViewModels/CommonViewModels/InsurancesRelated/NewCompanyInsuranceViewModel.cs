using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Insurances;
using SU.Frontend.Helper;
using SU.Frontend.Helper.DI_Objects.InsuranceObjects;
using SU.Frontend.Helper.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.CommonViewModels.NewInsurance
{
    public class NewCompanyInsuranceViewModel : ObservableObject
    {
        // Controller
        private readonly CompanyCustomerController _companyCustomerController;

        // Services
        private readonly IPolicyHolderService _policyHolderService;
        private readonly INavigationService _navigationService;

        // ObservableCollection to store the filtered list of company customers
        public ObservableCollection<CompanyCustomer> FilteredCompanyCustomers { get; set; }

        // The selected company customer from the DataGrid
        private CompanyCustomer _selectedCompanyCustomer;
        public CompanyCustomer SelectedCompanyCustomer
        {
            get => _selectedCompanyCustomer;
            set
            {
                _selectedCompanyCustomer = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSelectPolicyHolder)); // Notify if policy holder selection is possible
            }
        }

        // Search query for filtering the company customers
        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
                FilterCompanyCustomers(); // Filter the customers based on the search query
            }
        }

        // List to hold all company customers
        private List<CompanyCustomer> _allCompanyCustomers;

        // ICommand for selecting the policy holder
        public ICommand SelectPolicyHolderCommand { get; }

        public NewCompanyInsuranceViewModel(CompanyCustomerController companyCustomerController, INavigationService navigationService, IPolicyHolderService policyHolderService)
        {
            _companyCustomerController = companyCustomerController;
            _navigationService = navigationService;
            _policyHolderService = policyHolderService;
            FilteredCompanyCustomers = new ObservableCollection<CompanyCustomer>();

            // Initialize the command for selecting a policy holder
            SelectPolicyHolderCommand = new RelayCommand(OnSelectPolicyHolder, CanSelectPolicyHolder);

            // Load all company customers when the ViewModel is initialized
            LoadCompanyCustomersAsync();
        }

        // Method to load all company customers from the controller
        private async void LoadCompanyCustomersAsync()
        {
            var result = await _companyCustomerController.GetAllCompanyCustomers();
            if (result.companyCustomers.Any())
            {
                _allCompanyCustomers = result.companyCustomers;
                FilterCompanyCustomers();
            }
        }

        // Method to filter the customers based on the search query
        private void FilterCompanyCustomers()
        {
            var filteredList = _allCompanyCustomers
                .Where(c => string.IsNullOrEmpty(SearchQuery) ||
                            c.CompanyName.ToLower().Contains(SearchQuery.ToLower()) ||
                            c.OrganizationNumber.ToLower().Contains(SearchQuery.ToLower()) ||
                            c.ContactPerson.ToLower().Contains(SearchQuery.ToLower()) ||
                            c.CompanyEmailAdress.ToLower().Contains(SearchQuery.ToLower()) ||
                            c.ContactPersonPhonenumber.ToLower().Contains(SearchQuery.ToLower()) ||
                            c.CompanyCustomerId.ToString().ToLower().Contains(SearchQuery.ToLower()))
                .ToList();

            FilteredCompanyCustomers.Clear();
            foreach (var customer in filteredList)
            {
                FilteredCompanyCustomers.Add(customer);
            }
        }

        // Method to check if a policy holder can be selected
        public bool CanSelectPolicyHolder()
        {
            return SelectedCompanyCustomer != null;
        }

        // Method to handle the selection of a policy holder
        private void OnSelectPolicyHolder()
        {
            // Logic to set the SelectedCompanyCustomer as the policy holder for the insurance
            if (SelectedCompanyCustomer != null)
            {
                _policyHolderService.InsurancePolicyHolder = new InsurancePolicyHolder
                {
                    CompanyCustomer = SelectedCompanyCustomer
                };
            }

            _navigationService.NavigateTo("CompanyInsuranceTypeView", "CommonViews.NewInsurance");
        }
    }
}
