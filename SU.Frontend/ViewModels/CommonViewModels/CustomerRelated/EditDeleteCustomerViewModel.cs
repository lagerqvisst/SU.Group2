using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Frontend.Helper;

namespace SU.Frontend.ViewModels.CommonViewModels.CustomerRelated;

public class EditDeleteCustomerViewModel : ObservableObject
{
    private readonly CompanyCustomerController _companyCustomerController;

    //Controllers for the customers
    private readonly PrivateCustomerController _privateCustomerController;

    // Visibility control for company customer on the DataGrids
    private bool _isCompanyCustomerVisible;

    // Visibility control for private customer on the DataGrids
    private bool _isPrivateCustomerVisible;

    // Selected company customer
    private CompanyCustomer _selectedCompanyCustomer;

    // Selected private customer
    private PrivateCustomer _selectedPrivateCustomer;

    //Constructor
    public EditDeleteCustomerViewModel(PrivateCustomerController privateCustomerController,
        CompanyCustomerController companyCustomerController)
    {
        _privateCustomerController = privateCustomerController;
        _companyCustomerController = companyCustomerController;

        // Load customers async
        LoadCustomersAsync();

        SaveCustomerCommand = new RelayCommand(SaveCustomer);
        DeleteCustomerCommand = new RelayCommand(DeleteCustomer, CanDeleteCustomer);
    }

    // Commands to public properties
    public ICommand SaveCustomerCommand { get; }
    public ICommand DeleteCustomerCommand { get; }

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

    // Method to check if a customer can be saved, a customer has to be selected.
    private async void SaveCustomer()
    {
        var confirm = MessageBox.Show("Customer has been updated",
            "Confirm", MessageBoxButton.OK);

        if (SelectedPrivateCustomer != null)
            await _privateCustomerController.UpdatePrivateCustomer(SelectedPrivateCustomer);
        else if (SelectedCompanyCustomer != null)
            await _companyCustomerController.UpdateCompanyCustomer(SelectedCompanyCustomer);
    }

    // Method to check if a customer can be deleted, a customer has to be selected.
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

        // Continue with deletion if user confirms
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

    // Load both types of customers async
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