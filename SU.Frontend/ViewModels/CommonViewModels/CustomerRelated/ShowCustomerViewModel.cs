using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums.Insurance;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Frontend.ViewModels.CommonViewModels.CustomerRelated
{
    public class ShowCustomerViewModel : ObservableObject
    {
        private readonly PrivateCustomerController _privateCustomerController;
        private readonly CompanyCustomerController _companyCustomerController;

        // Separata listor för privat- och företagskunder
        public ObservableCollection<PrivateCustomer> PrivateCustomers { get; set; }
        public ObservableCollection<CompanyCustomer> CompanyCustomers { get; set; }

        // Vald privat- eller företagskund
        private object _selectedCustomer;
        public object SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged(); // Notifierar UI om förändringen
                _ = OnSelectedCustomerChangedAsync();
            }
        }

        // Egenskap för att hålla de valda detaljerna, som kan vara antingen PrivateCustomer eller CompanyCustomer
        private object _selectedCustomerDetails;
        public object SelectedCustomerDetails
        {
            get => _selectedCustomerDetails;
            set
            {
                _selectedCustomerDetails = value;
                OnPropertyChanged(); // Uppdaterar UI med de nya detaljerna
            }
        }

        // Konstruktorn
        public ShowCustomerViewModel(PrivateCustomerController privateCustomerController, CompanyCustomerController companyCustomerController)
        {
            _privateCustomerController = privateCustomerController;
            _companyCustomerController = companyCustomerController;

            // Hämta kunder från båda controllers
            _ = LoadCustomersAsync();
        }

        // Asynkron metod för att ladda kunder
        private async Task LoadCustomersAsync()
        {
            // Hämta privata kunder asynkront
            var privateCustomerResult = await _privateCustomerController.GetAllPrivateCustomers();
            PrivateCustomers = new ObservableCollection<PrivateCustomer>(privateCustomerResult.privateCustomers);

            // Hämta företagskunder asynkront
            var companyCustomerResult = await _companyCustomerController.GetAllCompanyCustomers();
            CompanyCustomers = new ObservableCollection<CompanyCustomer>(companyCustomerResult.companyCustomers);
        }

        // Asynkron metod för att hantera valet av en kund
        private async Task OnSelectedCustomerChangedAsync()
        {
            if (SelectedCustomer != null)
            {
                // Ladda detaljer beroende på vilken typ av kund som valts
                if (SelectedCustomer is PrivateCustomer privateCustomer)
                {
                    SelectedCustomerDetails = await LoadPrivateCustomerDetailsAsync(privateCustomer);
                }
                else if (SelectedCustomer is CompanyCustomer companyCustomer)
                {
                    SelectedCustomerDetails = await LoadCompanyCustomerDetailsAsync(companyCustomer);
                }
            }
        }

        // Ladda detaljer för en privatkund
        private async Task<PrivateCustomer> LoadPrivateCustomerDetailsAsync(PrivateCustomer privateCustomer)
        {
            await Task.Delay(500); // Simulera en laddningstid
            return _privateCustomerController.GetPrivateCustomerDetails(privateCustomer);
        }

        // Ladda detaljer för en företagskund
        private async Task<CompanyCustomer> LoadCompanyCustomerDetailsAsync(CompanyCustomer companyCustomer)
        {
            await Task.Delay(500); // Simulera en laddningstid
            return _companyCustomerController.GetCompanyCustomerDetails(companyCustomer);
        }
    }

}










}
