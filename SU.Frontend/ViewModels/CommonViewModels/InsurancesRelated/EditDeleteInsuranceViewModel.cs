using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances;
using SU.Frontend.Helper;
using SU.Frontend.Helper.DI_Objects.InsuranceObjects;
using SU.Frontend.Helper.DI_Objects.User;
using SU.Frontend.Helper.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace SU.Frontend.ViewModels.CommonViewModels.InsurancesRelated
{
    public class EditDeleteInsuranceViewModel : ObservableObject
    {

        private readonly INavigationService _navigationService;
        private readonly IPolicyHolderService _policyHolderService;
        private readonly ILoggedInUserService _loggedInSeller;
        private readonly InsuranceListingController _insuranceListingController;
        private readonly InsuranceCreateController _insuranceCreateController;

        public ICommand SaveInsuranceCommand { get; }
        public ICommand DeleteInsuranceCommand { get; }

        public List<InsuranceType> PrivateInsuranceTypes { get; set; }
        public List<PaymentPlan> PaymentPlans { get; set; }

        private InsuranceType _selectedInsuranceType;
        public InsuranceType SelectedInsuranceType
        {
            get => _selectedInsuranceType;
            set
            {
                _selectedInsuranceType = value;
                OnPropertyChanged();
            }
        }

        private PaymentPlan _selectedPaymentPlan;
        public PaymentPlan SelectedPaymentPlan
        {
            get => _selectedPaymentPlan;
            set
            {
                _selectedPaymentPlan = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedPaymentPlan));
            }
        }

        private bool IsPrivateInsuranceType(InsuranceType type)
        {
            return type == InsuranceType.ChildAccidentAndHealthInsurance
                || type == InsuranceType.AdultAccidentAndHealthInsurance
                || type == InsuranceType.AdultLifeInsurance;
        }


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
            }
        }

        public EditDeleteInsuranceViewModel(INavigationService navigationService, IPolicyHolderService policyHolderService, ILoggedInUserService loggedInUserService, InsuranceListingController insuranceListingController, InsuranceCreateController insuranceCreateController)
        {

            _navigationService = navigationService;
            _policyHolderService = policyHolderService;
            _loggedInSeller = loggedInUserService;
            _insuranceListingController = insuranceListingController;
            _insuranceCreateController = insuranceCreateController;

            Task.Run(async () => await LoadInsurancesAsync()).Wait();

            PrivateInsuranceTypes = Enum.GetValues(typeof(InsuranceType))
           .Cast<InsuranceType>()
           .Where(t => IsPrivateInsuranceType(t))
           .ToList();

            PaymentPlans = Enum.GetValues(typeof(PaymentPlan))
           .Cast<PaymentPlan>()
           .ToList();


            SaveInsuranceCommand = new RelayCommand(SaveInsurance);
            DeleteInsuranceCommand = new RelayCommand(DeleteInsurance, CanDeleteInsurance);
        }

        private async void SaveInsurance()
        {
            var confirm = MessageBox.Show("Insurance has been updated",
                                  "Confirm", MessageBoxButton.OK);

            if (SelectedInsurance != null)
            {
                await _insuranceCreateController.UpdateInsurance(SelectedInsurance);
            }
        }

        private bool CanDeleteInsurance()
        {
            return SelectedInsurance != null;
        }

        private async void DeleteInsurance()
        {
            var confirm = MessageBox.Show("Are you sure you want to delete this insurance?",
                                  "Confirm Delete",
                                  MessageBoxButton.YesNo);

            if (confirm != MessageBoxResult.Yes) return;

            // Fortsätt med att ta bort om användaren bekräftar
            if (SelectedInsurance != null)
            {
                await _insuranceCreateController.DeleteInsurance(SelectedInsurance);
                Insurances.Remove(SelectedInsurance);
                SelectedInsurance = null;
            }
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

    }
}
