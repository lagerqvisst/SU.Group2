using System.Windows;
using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Frontend.Helper;

namespace SU.Frontend.ViewModels.CommonViewModels.Customers;

public class NewPrivateCustomerViewModel : ObservableObject
{
    // Controller for the private customer
    private readonly PrivateCustomerController _privateCustomerController;

    private string _address;

    // Button content
    private string _buttonContent = "Register";

    private string _email;

    private string _firstName;

    // Loading state
    private bool _isLoading;

    private string _lastName;

    // Properties bound to the XAML form fields
    private string _personalNumber;

    private string _phoneNumber;

    // Constructor
    public NewPrivateCustomerViewModel(PrivateCustomerController privateCustomerController)
    {
        _privateCustomerController = privateCustomerController;
        RegisterPrivateCustomerCommand =
            new RelayCommand(async () => await RegisterPrivateCustomer(), CanRegisterPrivateCustomer);
    }

    public string PersonalNumber
    {
        get => _personalNumber;
        set
        {
            _personalNumber = value;
            OnPropertyChanged();
        }
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged();
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged();
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            _phoneNumber = value;
            OnPropertyChanged();
        }
    }

    public string Address
    {
        get => _address;
        set
        {
            _address = value;
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
    public RelayCommand RegisterPrivateCustomerCommand { get; }

    // Validate that all fields are filled in
    private bool CanRegisterPrivateCustomer()
    {
        return !string.IsNullOrWhiteSpace(PersonalNumber)
               && !string.IsNullOrWhiteSpace(FirstName)
               && !string.IsNullOrWhiteSpace(LastName)
               && !string.IsNullOrWhiteSpace(Email)
               && !string.IsNullOrWhiteSpace(PhoneNumber)
               && !string.IsNullOrWhiteSpace(Address);
    }

    // Registration logic using the controller
    private async Task RegisterPrivateCustomer()
    {
        IsLoading = true; // Start loading
        try
        {
            var newCustomer = new PrivateCustomer
            {
                PersonalNumber = PersonalNumber,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Address = Address
            };

            // Simulate a small delay to show the loading effect
            await Task.Delay(100);

            var result = await _privateCustomerController.CreateNewPrivateCustomer(newCustomer);

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