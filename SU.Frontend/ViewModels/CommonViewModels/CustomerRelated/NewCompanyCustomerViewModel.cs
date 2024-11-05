using System.Windows;
using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Frontend.Helper;

namespace SU.Frontend.ViewModels.CommonViewModels.Customers;

public class NewCompanyCustomerViewModel : ObservableObject
{
    // Controller for the company customer
    private readonly CompanyCustomerController _companyCustomerController;

    // Button content
    private string _buttonContent = "Register";

    private string _companyAddress;

    private string _companyEmailAddress;

    private string _companyName;

    private string _companyPhoneNumber;

    private string _contactPerson;

    private string _contactPersonPhoneNumber;

    // Loading state
    private bool _isLoading;

    // Properties bound to the XAML form fields
    private string _organizationNumber;

    // Constructor
    public NewCompanyCustomerViewModel(CompanyCustomerController companyCustomerController)
    {
        _companyCustomerController = companyCustomerController;
        RegisterCompanyCustomerCommand =
            new RelayCommand(async () => await RegisterCompanyCustomer(), CanRegisterCompanyCustomer);
    }

    public string OrganizationNumber
    {
        get => _organizationNumber;
        set
        {
            _organizationNumber = value;
            OnPropertyChanged();
        }
    }

    public string CompanyName
    {
        get => _companyName;
        set
        {
            _companyName = value;
            OnPropertyChanged();
        }
    }

    public string ContactPerson
    {
        get => _contactPerson;
        set
        {
            _contactPerson = value;
            OnPropertyChanged();
        }
    }

    public string ContactPersonPhoneNumber
    {
        get => _contactPersonPhoneNumber;
        set
        {
            _contactPersonPhoneNumber = value;
            OnPropertyChanged();
        }
    }

    public string CompanyAddress
    {
        get => _companyAddress;
        set
        {
            _companyAddress = value;
            OnPropertyChanged();
        }
    }

    public string CompanyPhoneNumber
    {
        get => _companyPhoneNumber;
        set
        {
            _companyPhoneNumber = value;
            OnPropertyChanged();
        }
    }



    public string CompanyEmailAddress
    {
        get => _companyEmailAddress;
        set
        {
            _companyEmailAddress = value;
            OnPropertyChanged();
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
            ButtonContent = _isLoading ? "Loading..." : "Register";
        }
    }

    public string ButtonContent
    {
        get => _buttonContent;
        set
        {
            _buttonContent = value;
            OnPropertyChanged();
        }
    }

    // ICommand for the registration button
    public RelayCommand RegisterCompanyCustomerCommand { get; }

    // Validate that all fields are filled in
    private bool CanRegisterCompanyCustomer()
    {
        return !string.IsNullOrWhiteSpace(OrganizationNumber)
               && !string.IsNullOrWhiteSpace(CompanyName)
               && !string.IsNullOrWhiteSpace(ContactPerson)
               && !string.IsNullOrWhiteSpace(ContactPersonPhoneNumber)
               && !string.IsNullOrWhiteSpace(CompanyAddress)
               && !string.IsNullOrWhiteSpace(CompanyEmailAddress);
    }

    // Registration logic using the controller
    private async Task RegisterCompanyCustomer()
    {
        IsLoading = true; // Start loading
        try
        {
            var newCustomer = new CompanyCustomer
            {
                OrganizationNumber = OrganizationNumber,
                CompanyName = CompanyName,
                ContactPerson = ContactPerson,
                ContactPersonPhonenumber = ContactPersonPhoneNumber,
                CompanyAdress = CompanyAddress,
                CompanyPhoneNumber = CompanyPhoneNumber,
                CompanyEmailAdress = CompanyEmailAddress
            };

            // Simulate a small delay to show the loading effect
            await Task.Delay(100);

            var result = await _companyCustomerController.CreateCompanyCustomer(newCustomer);

            if (result.success)
                MessageBox.Show(result.message, "Registration Successful", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            else
                MessageBox.Show(result.message, "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An unexpected error occurred: {ex.Message}");
        }
        finally
        {
            IsLoading = false; // Stop loading
        }
    }
}