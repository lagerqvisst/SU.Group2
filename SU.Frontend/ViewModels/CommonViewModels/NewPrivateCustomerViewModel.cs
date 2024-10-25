using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Frontend.Helper;

namespace SU.Frontend.ViewModels.CommonViewModels
{
    public class NewPrivateCustomerViewModel : ObservableObject
    {
        private readonly PrivateCustomerController _privateCustomerController;

        // Properties bound to the XAML form fields
        private string _personalNumber;
        public string PersonalNumber
        {
            get => _personalNumber;
            set
            {
                _personalNumber = value;
                OnPropertyChanged();
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        // Loading state
        private bool _isLoading;
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

        // Button content
        private string _buttonContent = "Register";
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

        public NewPrivateCustomerViewModel(PrivateCustomerController privateCustomerController)
        {
            _privateCustomerController = privateCustomerController;
            RegisterPrivateCustomerCommand = new RelayCommand(async () => await RegisterPrivateCustomer(), CanRegisterPrivateCustomer);
        }

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

                if (result.Success)
                {
                    MessageBox.Show(result.Message, "Registration Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(result.Message, "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

}
