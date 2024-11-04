using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Insurances;
using SU.Frontend.Helper;
using SU.Frontend.Helper.DI_Objects.InsuranceObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Frontend.ViewModels.CommonViewModels.InsurancesRelated
{
    public class ShowInsuranceViewModel : ObservableObject
    {
        private readonly InsuranceListingController _insuranceListingController;

        // ObservableCollections for insurances
        public ObservableCollection<Insurance> Insurances { get; set; } = new ObservableCollection<Insurance>();

        // Chosen insurance
        private Insurance _selectedInsurance;
        public Insurance SelectedInsurance
        {
            get => _selectedInsurance;
            set
            {
                _selectedInsurance = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedInsuranceAsCollection)); // Update DataGrid

                UpdateCustomerName(); // Update policy holder name
                OnPropertyChanged(nameof(InsurancePolicyHolder)); // Update UI
            }
        }

        public ShowInsuranceViewModel(InsuranceListingController insuranceListingController)
        {
            _insuranceListingController = insuranceListingController;

            Task.Run(async () => await LoadInsurancesAsync()).Wait();
        }

        
        private async Task LoadInsurancesAsync()
        {
            await LoadAllInsurancesAsync();
        }

        private async Task LoadAllInsurancesAsync()
        {
            var insuranceResult = await _insuranceListingController.GetAllInsurances();
            if (insuranceResult.insurances?.Any() ?? false)
            {
                Insurances.Clear();
                foreach (var insurance in insuranceResult.insurances)
                {
                    Insurances.Add(insurance);
                }
            }
            else
            {
                Console.WriteLine("No insurances found");
            }
        }


        public IEnumerable<Insurance> SelectedInsuranceAsCollection
        {
            get
            {
                if (SelectedInsurance != null)
                    return new List<Insurance> { SelectedInsurance };
                return null;
            }
        }

        private string insurancePolicyHolderName;
        public string InsurancePolicyHolder
        {
            get => insurancePolicyHolderName;
            set
            {
                insurancePolicyHolderName = value;
                OnPropertyChanged(nameof(InsurancePolicyHolder));
            }
        }

        private void UpdateCustomerName()
        {
            if (SelectedInsurance != null)
            {
                var insurancePolicyHolder = SelectedInsurance.InsurancePolicyHolder;

                if (insurancePolicyHolder?.PrivateCustomer != null)
                {
                    insurancePolicyHolderName = $"{insurancePolicyHolder.PrivateCustomer.FirstName} {insurancePolicyHolder.PrivateCustomer.LastName}";
                }
                else if (insurancePolicyHolder?.CompanyCustomer != null)
                {
                    insurancePolicyHolderName = insurancePolicyHolder.CompanyCustomer.CompanyName;
                }
                else
                {
                    insurancePolicyHolderName = "Unknown";
                }
            }
            else
            {
                insurancePolicyHolderName = " ";
            }
        }

    }
}