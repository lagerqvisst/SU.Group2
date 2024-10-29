using SU.Backend.Controllers;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Enums.Insurance.Addons;
using SU.Backend.Models.Insurances;
using SU.Backend.Models.Insurances.Coverage;
using SU.Frontend.Helper;
using SU.Frontend.Helper.DI_Objects.InsuranceObjects;
using SU.Frontend.Helper.DI_Objects.User;
using SU.Frontend.Helper.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.CommonViewModels.NewInsurance
{
    public class PrivateInsuranceTypeViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IPolicyHolderService _policyHolderService;
        private readonly ILoggedInUserService _loggedInSeller;
        private readonly InsuranceListingController _insuranceListingController;
        private readonly InsuranceCreateController _insuranceCreateController;


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
                _ = OnSelectedInsuranceTypeChangedAsync();

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
            }
        }

        // Property for Note
        private string _note;
        public string Note
        {
            get => _note;
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        // Checkbox control
        #region Checkbox control
        private bool _isInsuredPersonSameAsPolicyHolder = true; // Default to true
        public bool IsInsuredPersonSameAsPolicyHolder
        {
            get => _isInsuredPersonSameAsPolicyHolder;
            set
            {
                _isInsuredPersonSameAsPolicyHolder = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsInsuredPersonFieldsVisible));

                // If they are the same, populate the InsuredPerson details with PolicyHolder's info
                if (_isInsuredPersonSameAsPolicyHolder)
                {
                    InsuredPersonName = _policyHolderService.InsurancePolicyHolder.PrivateCustomer?.FirstName + " " +
                                        _policyHolderService.InsurancePolicyHolder.PrivateCustomer?.LastName;
                    InsuredPersonPersonalNumber = _policyHolderService.InsurancePolicyHolder.PrivateCustomer?.PersonalNumber;
                }
                else
                {
                    InsuredPersonName = string.Empty;
                    InsuredPersonPersonalNumber = string.Empty;
                }
            }
        }

        public bool IsInsuredPersonFieldsVisible => !IsInsuredPersonSameAsPolicyHolder;

        // InsuredPerson properties
        private string _insuredPersonName;
        public string InsuredPersonName
        {
            get => _insuredPersonName;
            set
            {
                _insuredPersonName = value;
                OnPropertyChanged();
            }
        }

        private string _insuredPersonPersonalNumber;
        public string InsuredPersonPersonalNumber
        {
            get => _insuredPersonPersonalNumber;
            set
            {
                _insuredPersonPersonalNumber = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private bool _isAddonVisible;
        public bool IsAddonVisible
        {
            get => _isAddonVisible;
            set
            {
                _isAddonVisible = value;
                OnPropertyChanged();
            }
        }



        private List<InsuranceAddonType> _sicknessAccidentAddons;
        public List<InsuranceAddonType> SicknessAccidentAddons
        {
            get => _sicknessAccidentAddons;
            set
            {
                _sicknessAccidentAddons = value;
                OnPropertyChanged();
            }
        }

        private List<InsuranceAddonType> _longTermSicknessAddons;
        public List<InsuranceAddonType> LongTermSicknessAddons
        {
            get => _longTermSicknessAddons;
            set
            {
                _longTermSicknessAddons = value;
                OnPropertyChanged();
            }
        }

        // Selected add-ons
        private InsuranceAddonType _selectedSicknessAccidentAddon;
        public InsuranceAddonType SelectedSicknessAccidentAddon
        {
            get => _selectedSicknessAccidentAddon;
            set
            {
                _selectedSicknessAccidentAddon = value;
                OnPropertyChanged();
            }
        }

        private InsuranceAddonType _selectedLongTermSicknessAddon;
        public InsuranceAddonType SelectedLongTermSicknessAddon
        {
            get => _selectedLongTermSicknessAddon;
            set
            {
                _selectedLongTermSicknessAddon = value;
                OnPropertyChanged();
            }
        }


        private List<PrivateCoverageOption> _availableCoverageOptions;
        public List<PrivateCoverageOption> AvailableCoverageOptions
        {
            get => _availableCoverageOptions;
            set
            {
                _availableCoverageOptions = value;
                OnPropertyChanged();
            }
        }

        private PrivateCoverageOption _selectedCoverageOption;
        public PrivateCoverageOption SelectedCoverageOption
        {
            get => _selectedCoverageOption;
            set
            {
                _selectedCoverageOption = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }

        public string DisplayYearText => $"Available options for {SelectedInsuranceType.ToString()} & {DateTime.Now.Year}";

        public ICommand ContinueCommand { get; }

        public bool IsCoverageOptionsEnabled => AvailableCoverageOptions?.Any() == true;

        public ICommand CreateInsuranceCommand { get; }


        public PrivateInsuranceTypeViewModel(INavigationService navigationService, IPolicyHolderService policyHolderService, ILoggedInUserService loggedInUserService, InsuranceListingController insuranceListingController, InsuranceCreateController insuranceCreateController)
        {
            _navigationService = navigationService;
            _policyHolderService = policyHolderService;
            _loggedInSeller = loggedInUserService;
            _insuranceListingController = insuranceListingController;
            _insuranceCreateController = insuranceCreateController;

            // Sätt standardvärden för start- och slutdatum
            StartDate = DateTime.Now;
            EndDate = StartDate.Value.AddYears(1);

            // Initialize the lists used in ComboBoxes
            PrivateInsuranceTypes = Enum.GetValues(typeof(InsuranceType))
                .Cast<InsuranceType>()
                .Where(t => IsPrivateInsuranceType(t))
                .ToList();

            PaymentPlans = Enum.GetValues(typeof(PaymentPlan))
                .Cast<PaymentPlan>()
                .ToList();

            // Choose the first private insurance type as default
            if (PrivateInsuranceTypes.Any())
            {
                SelectedInsuranceType = PrivateInsuranceTypes.First(); // This triggers LoadCoverageOptions method to populate listview
            }

            // Load monthly payment plan as default
            if (PaymentPlans.Any()) { SelectedPaymentPlan = PaymentPlan.Monthly; }

            _insuranceCreateController = insuranceCreateController;

            CreateInsuranceCommand = new RelayCommand(CreateInsurance, CanCreateInsurance);
        }


        private bool IsPrivateInsuranceType(InsuranceType type)
        {
            return type == InsuranceType.ChildAccidentAndHealthInsurance
                || type == InsuranceType.AdultAccidentAndHealthInsurance
                || type == InsuranceType.AdultLifeInsurance;
        }

        private async Task OnSelectedInsuranceTypeChangedAsync()
        {
            // Uppdatera IsAddonVisible baserat på valt försäkringstyp
            IsAddonVisible = SelectedInsuranceType == InsuranceType.ChildAccidentAndHealthInsurance ||
                             SelectedInsuranceType == InsuranceType.AdultAccidentAndHealthInsurance;

            SelectedLongTermSicknessAddon = null;
            SelectedSicknessAccidentAddon = null;
            SelectedCoverageOption = null;
            // Kör metoder för att ladda alternativ och tillägg
            await LoadCoverageOptions();
            await LoadAddonOptions();
        }

        private async Task LoadCoverageOptions()
        {
            if (SelectedInsuranceType != null)
            {
                var (options, message) = await _insuranceListingController.GetSpecificPrivateOption(SelectedInsuranceType);

                // Sätt AvailableCoverageOptions direkt till de hämtade alternativen
                AvailableCoverageOptions = options;

                // Meddela UI att AvailableCoverageOptions har uppdaterats
                OnPropertyChanged(nameof(AvailableCoverageOptions));
                OnPropertyChanged(nameof(IsCoverageOptionsEnabled));
                OnPropertyChanged(nameof(DisplayYearText));
            }
        }

        private async Task LoadAddonOptions()
        {
            if (SelectedInsuranceType == InsuranceType.ChildAccidentAndHealthInsurance || SelectedInsuranceType == InsuranceType.AdultAccidentAndHealthInsurance)
            {
                var (success, addons, message) = await _insuranceListingController.GetAllInsuranceAddonTypes();

                if (success)
                {
                    // Filtrera de två olika typerna av tillägg
                    SicknessAccidentAddons = addons.Where(a => a.Description == AddonType.SicknessAccident).ToList();
                    LongTermSicknessAddons = addons.Where(a => a.Description == AddonType.LongTermSickness).ToList();
                }
                else
                {
                    SicknessAccidentAddons = new List<InsuranceAddonType>();
                    LongTermSicknessAddons = new List<InsuranceAddonType>();
                }

                OnPropertyChanged(nameof(SicknessAccidentAddons));
                OnPropertyChanged(nameof(LongTermSicknessAddons));
            }
            else
            {
                SicknessAccidentAddons = new List<InsuranceAddonType>();
                LongTermSicknessAddons = new List<InsuranceAddonType>();
            }
        }

        private async void CreateInsurance()
        {
            // Gather necessary parameters from UI to pass to CreatePrivateInsurance
            var privateCustomer = _policyHolderService.InsurancePolicyHolder.PrivateCustomer;
            var insuranceType = SelectedInsuranceType;
            var privateCoverageOption = SelectedCoverageOption;
            var seller = _loggedInSeller.LoggedInEmployee;
            var isPolicyHolderInsured = IsInsuredPersonSameAsPolicyHolder;
            var note = Note;
            var paymentPlan = SelectedPaymentPlan;
            var startDate = StartDate ?? DateTime.Now; // Använd valt startdatum eller nuvarande datum
            var endDate = EndDate ?? startDate.AddYears(1); // Använd valt slutdatum eller ett år framåt som standard 
            var addons = new List<InsuranceAddonType> { SelectedSicknessAccidentAddon, SelectedLongTermSicknessAddon }
                         .Where(addon => addon != null).ToList();
            var insuredPerson = !IsInsuredPersonSameAsPolicyHolder
                                ? new InsuredPerson { InsuredPersonName = InsuredPersonName, InsuredPersonPersonalNumber = InsuredPersonPersonalNumber }
                                : null;

            // Call the controller method
            var result = await _insuranceCreateController.CreatePrivateInsurance(privateCustomer, insuranceType, privateCoverageOption, seller, isPolicyHolderInsured, note, paymentPlan, startDate, endDate, addons, insuredPerson);

            if (result.success)
            {
                MessageBox.Show($"{result.message}");
            }
            else
            {
                MessageBox.Show($"{result.message}");
            }
        }

        private bool CanCreateInsurance()
        {
            return SelectedInsuranceType != null
                   && SelectedCoverageOption != null
                   && SelectedPaymentPlan != null;

        }
    }
}
