using System.Collections.ObjectModel;
using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Frontend.Helper;

namespace SU.Frontend.ViewModels.CommonViewModels.CustomerRelated;

public class ShowCustomerViewModel : ObservableObject
{
    private readonly CompanyCustomerController _companyCustomerController;

    // Controllers for customers
    private readonly PrivateCustomerController _privateCustomerController;

    private bool _isCompanyCustomerVisible;

    // Visibility properties for the DataGrids
    private bool _isPrivateCustomerVisible;

    private CompanyCustomer _selectedCompanyCustomer;

    // Chosen customers
    private PrivateCustomer _selectedPrivateCustomer;

    private string _privateCustomerSearchText;
    private string _companyCustomerSearchText;


    public IEnumerable<PrivateCustomer> FilteredPrivateCustomers =>
        PrivateCustomers.Where(c => string.IsNullOrEmpty(PrivateCustomerSearchText) ||
                                    c.FirstName.Contains(PrivateCustomerSearchText, StringComparison.OrdinalIgnoreCase) ||
                                    c.PersonalNumber.Contains(PrivateCustomerSearchText, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<CompanyCustomer> FilteredCompanyCustomers =>
        CompanyCustomers.Where(c => string.IsNullOrEmpty(CompanyCustomerSearchText) ||
                                    c.CompanyName.Contains(CompanyCustomerSearchText, StringComparison.OrdinalIgnoreCase) ||
                                    c.OrganizationNumber.Contains(CompanyCustomerSearchText, StringComparison.OrdinalIgnoreCase));

    public string PrivateCustomerSearchText
    {
        get => _privateCustomerSearchText;
        set
        {
            _privateCustomerSearchText = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FilteredPrivateCustomers));

            // Sätt första objektet som förvalt om det finns några resultat
            SelectedPrivateCustomer = FilteredPrivateCustomers.FirstOrDefault();
        }
    }

    public string CompanyCustomerSearchText
    {
        get => _companyCustomerSearchText;
        set
        {
            _companyCustomerSearchText = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FilteredCompanyCustomers));

            // Sätt första objektet som förvalt om det finns några resultat
            SelectedCompanyCustomer = FilteredCompanyCustomers.FirstOrDefault();
        }
    }

    // Constructor
    public ShowCustomerViewModel(PrivateCustomerController privateCustomerController,
        CompanyCustomerController companyCustomerController)
    {
        _privateCustomerController = privateCustomerController;
        _companyCustomerController = companyCustomerController;

        // Ladda kunder asynkront
        LoadCustomersAsync();
    }

    // ObservableCollections for customers
    public ObservableCollection<PrivateCustomer> PrivateCustomers { get; set; } = new();
    public ObservableCollection<CompanyCustomer> CompanyCustomers { get; set; } = new();

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

    public bool IsPrivateCustomerVisible
    {
        get => _isPrivateCustomerVisible;
        set
        {
            _isPrivateCustomerVisible = value;
            OnPropertyChanged();
        }
    }

    public bool IsCompanyCustomerVisible
    {
        get => _isCompanyCustomerVisible;
        set
        {
            _isCompanyCustomerVisible = value;
            OnPropertyChanged();
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
        foreach (var customer in privateCustomerResult.privateCustomers) PrivateCustomers.Add(customer);
    }

    private async Task LoadCompanyCustomersAsync()
    {
        // Get company customers async and add them to ObservableCollection
        var companyCustomerResult = await _companyCustomerController.GetAllCompanyCustomers();
        CompanyCustomers.Clear();
        foreach (var customer in companyCustomerResult.companyCustomers) CompanyCustomers.Add(customer);
    }
}