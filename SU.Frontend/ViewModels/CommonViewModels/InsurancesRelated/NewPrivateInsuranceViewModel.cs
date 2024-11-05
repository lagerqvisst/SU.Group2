using System.Collections.ObjectModel;
using System.Windows.Input;
using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Insurances;
using SU.Frontend.Helper;
using SU.Frontend.Helper.DI_Objects.InsuranceObjects;
using SU.Frontend.Helper.Navigation;

namespace SU.Frontend.ViewModels.CommonViewModels.NewInsurance;

public class NewPrivateInsuranceViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    // Services
    private readonly IPolicyHolderService _policyHolderService;

    // Controller
    private readonly PrivateCustomerController _privateCustomerController;

    // List to hold all private customers
    private List<PrivateCustomer> _allPrivateCustomers;

    // Search query for filtering the private customers
    private string _searchQuery;

    // The selected private customer from the DataGrid
    private PrivateCustomer _selectedPrivateCustomer;

    public NewPrivateInsuranceViewModel(PrivateCustomerController privateCustomerController,
        INavigationService navigationService, IPolicyHolderService policyHolderService)
    {
        _privateCustomerController = privateCustomerController;
        _navigationService = navigationService; // Injected NavigationService
        _policyHolderService = policyHolderService; // Injected PolicyHolderService
        FilteredPrivateCustomers = new ObservableCollection<PrivateCustomer>();

        // Initialize the command for selecting a policy holder
        SelectPolicyHolderCommand = new RelayCommand(OnSelectPolicyHolder, CanSelectPolicyHolder);

        // Load all private customers when the ViewModel is initialized
        LoadPrivateCustomersAsync();
    }

    // ObservableCollection to store the filtered list of private customers
    public ObservableCollection<PrivateCustomer> FilteredPrivateCustomers { get; set; }

    public PrivateCustomer SelectedPrivateCustomer
    {
        get => _selectedPrivateCustomer;
        set
        {
            _selectedPrivateCustomer = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanSelectPolicyHolder)); // Notify if policy holder selection is possible
        }
    }

    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            _searchQuery = value;
            OnPropertyChanged();
            FilterPrivateCustomers(); // Filter the customers based on the search query
        }
    }

    // ICommand for selecting the policy holder
    public ICommand SelectPolicyHolderCommand { get; }

    // Method to load all private customers from the controller
    private async void LoadPrivateCustomersAsync()
    {
        var result = await _privateCustomerController.GetAllPrivateCustomers();
        if (result.privateCustomers.Any())
        {
            _allPrivateCustomers = result.privateCustomers;
            FilterPrivateCustomers();
        }
    }

    // Method to filter the customers based on the search query
    private void FilterPrivateCustomers()
    {
        var filteredList = _allPrivateCustomers
            .Where(c => string.IsNullOrEmpty(SearchQuery) ||
                        c.FirstName.ToLower().Contains(SearchQuery.ToLower()) ||
                        c.LastName.ToLower().Contains(SearchQuery.ToLower()) ||
                        c.Email.ToLower().Contains(SearchQuery.ToLower()) ||
                        c.PersonalNumber.ToLower().Contains(SearchQuery.ToLower()) ||
                        c.PhoneNumber.ToLower().Contains(SearchQuery.ToLower()) ||
                        c.PrivateCustomerId.ToString().ToLower().Contains(SearchQuery.ToLower()))
            .ToList();

        FilteredPrivateCustomers.Clear();
        foreach (var customer in filteredList) FilteredPrivateCustomers.Add(customer);
    }

    // Method to check if a policy holder can be selected
    public bool CanSelectPolicyHolder()
    {
        return SelectedPrivateCustomer != null;
    }

    // Method to handle the selection of a policy holder
    private void OnSelectPolicyHolder()
    {
        // Logic to set the SelectedPrivateCustomer as the policy holder for the insurance
        if (SelectedPrivateCustomer != null)
            _policyHolderService.InsurancePolicyHolder = new InsurancePolicyHolder
            {
                PrivateCustomer = SelectedPrivateCustomer
            };

        _navigationService.NavigateTo("PrivateCoverageTypeOptionView", "CommonViews.NewInsurance");
    }
}