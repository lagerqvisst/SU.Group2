using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Frontend.Helper;

namespace SU.Frontend.ViewModels.CommonViewModels
{
    public class NewCompanyCustomerViewModel : ObservableObject
    {
        private readonly CompanyCustomerController _companyCustomerController;

        // Properties bound to the XAML form fields
        private string _organizationNumber;
        public string OrganizationNumber
        {
            get => _organizationNumber;
            set
            {
                _organizationNumber = value;
                OnPropertyChanged();
            }
        }

        private string _companyName;
        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                OnPropertyChanged();
            }
        }

        private string _contactPerson;
        public string ContactPerson
        {
            get => _contactPerson;
            set
            {
                _contactPerson = value;
                OnPropertyChanged();
            }
        }

        private string _contactPersonPhoneNumber;
        public string ContactPersonPhoneNumber
        {
            get => _contactPersonPhoneNumber;
            set
            {
                _contactPersonPhoneNumber = value;
                OnPropertyChanged();
            }
        }

        private string _companyAddress;
        public string CompanyAddress
        {
            get => _companyAddress;
            set
            {
                _companyAddress = value;
                OnPropertyChanged();
            }
        }

        private string _companyPhoneNumber;
        public string CompanyPhoneNumber
        {
            get => _companyPhoneNumber;
            set
            {
                _companyPhoneNumber = value;
                OnPropertyChanged();
            }
        }

        private string _companyLandlineNumber;
        public string CompanyLandlineNumber
        {
            get => _companyLandlineNumber;
            set
            {
                _companyLandlineNumber = value;
                OnPropertyChanged();
            }
        }

        private string _companyEmailAddress;
        public string CompanyEmailAddress
        {
            get => _companyEmailAddress;
            set
            {
                _companyEmailAddress = value;
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
        public RelayCommand RegisterCompanyCustomerCommand { get; }

        public NewCompanyCustomerViewModel(CompanyCustomerController companyCustomerController)
        {
            _companyCustomerController = companyCustomerController;
            RegisterCompanyCustomerCommand = new RelayCommand(async () => await RegisterCompanyCustomer(), CanRegisterCompanyCustomer);
        }

        // Validate that all fields are filled in
        private bool CanRegisterCompanyCustomer()
        {
            return !string.IsNullOrWhiteSpace(OrganizationNumber)
                && !string.IsNullOrWhiteSpace(CompanyName)
                && !string.IsNullOrWhiteSpace(ContactPerson)
                && !string.IsNullOrWhiteSpace(ContactPersonPhoneNumber)
                && !string.IsNullOrWhiteSpace(CompanyAddress)
                && !string.IsNullOrWhiteSpace(CompanyPhoneNumber)
                && !string.IsNullOrWhiteSpace(CompanyLandlineNumber)
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
                    CompanyLandlineNumber = CompanyLandlineNumber,
                    CompanyEmailAdress = CompanyEmailAddress
                };

                // Simulate a small delay to show the loading effect
                await Task.Delay(100);

                var result = await _companyCustomerController.CreateCompanyCustomer(newCustomer);

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
