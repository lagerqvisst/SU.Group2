using System.Windows;
using System.Windows.Input;
using SU.Backend.Controllers;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Enums.Insurance.Addons;
using SU.Backend.Models.Insurances;
using SU.Backend.Models.Insurances.Coverage;
using SU.Frontend.Helper;
using SU.Frontend.Helper.DI_Objects.InsuranceObjects;
using SU.Frontend.Helper.DI_Objects.User;
using SU.Frontend.Helper.Navigation;

namespace SU.Frontend.ViewModels.CommonViewModels.NewInsurance;

public class PrivateInsuranceTypeViewModel : ObservableObject
{
    private readonly InsuranceCreateController _insuranceCreateController;

    // Controllers
    private readonly InsuranceListingController _insuranceListingController;

    private readonly ILoggedInUserService _loggedInSeller;

    // Services
    private readonly INavigationService _navigationService;
    private readonly IPolicyHolderService _policyHolderService;


    private List<PrivateCoverageOption> _availableCoverageOptions;

    private DateTime? _endDate;

    private bool _isAddonVisible;

    private List<InsuranceAddonType> _longTermSicknessAddons;

    // Property for Note
    private string _note;

    private PrivateCoverageOption _selectedCoverageOption;


    private InsuranceType _selectedInsuranceType;

    private InsuranceAddonType _selectedLongTermSicknessAddon;

    private PaymentPlan _selectedPaymentPlan;

    // Selected add-ons
    private InsuranceAddonType _selectedSicknessAccidentAddon;

    private List<InsuranceAddonType> _sicknessAccidentAddons;

    private DateTime? _startDate;

    // Constructor
    public PrivateInsuranceTypeViewModel(INavigationService navigationService, IPolicyHolderService policyHolderService,
        ILoggedInUserService loggedInUserService, InsuranceListingController insuranceListingController,
        InsuranceCreateController insuranceCreateController)
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
            SelectedInsuranceType =
                PrivateInsuranceTypes.First(); // This triggers LoadCoverageOptions method to populate listview

        // Load monthly payment plan as default
        if (PaymentPlans.Any()) SelectedPaymentPlan = PaymentPlan.Monthly;

        _insuranceCreateController = insuranceCreateController;

        CreateInsuranceCommand = new RelayCommand(CreateInsurance, CanCreateInsurance);
    }

    // Lists
    public List<InsuranceType> PrivateInsuranceTypes { get; set; }
    public List<PaymentPlan> PaymentPlans { get; set; }

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

    public PaymentPlan SelectedPaymentPlan
    {
        get => _selectedPaymentPlan;
        set
        {
            _selectedPaymentPlan = value;
            OnPropertyChanged();
        }
    }

    public string Note
    {
        get => _note;
        set
        {
            _note = value;
            OnPropertyChanged();
        }
    }

    public bool IsAddonVisible
    {
        get => _isAddonVisible;
        set
        {
            _isAddonVisible = value;
            OnPropertyChanged();
        }
    }

    public List<InsuranceAddonType> SicknessAccidentAddons
    {
        get => _sicknessAccidentAddons;
        set
        {
            _sicknessAccidentAddons = value;
            OnPropertyChanged();
        }
    }

    public List<InsuranceAddonType> LongTermSicknessAddons
    {
        get => _longTermSicknessAddons;
        set
        {
            _longTermSicknessAddons = value;
            OnPropertyChanged();
        }
    }

    public InsuranceAddonType SelectedSicknessAccidentAddon
    {
        get => _selectedSicknessAccidentAddon;
        set
        {
            _selectedSicknessAccidentAddon = value;
            OnPropertyChanged();
        }
    }

    public InsuranceAddonType SelectedLongTermSicknessAddon
    {
        get => _selectedLongTermSicknessAddon;
        set
        {
            _selectedLongTermSicknessAddon = value;
            OnPropertyChanged();
        }
    }

    public List<PrivateCoverageOption> AvailableCoverageOptions
    {
        get => _availableCoverageOptions;
        set
        {
            _availableCoverageOptions = value;
            OnPropertyChanged();
        }
    }

    public PrivateCoverageOption SelectedCoverageOption
    {
        get => _selectedCoverageOption;
        set
        {
            _selectedCoverageOption = value;
            OnPropertyChanged();
        }
    }

    public DateTime? StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;
            OnPropertyChanged();
        }
    }

    public DateTime? EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            OnPropertyChanged();
        }
    }

    // Display text for the year
    public string DisplayYearText => $"Available options for {SelectedInsuranceType.ToString()} & {DateTime.Now.Year}";

    // Commands
    public ICommand ContinueCommand { get; }

    public ICommand CreateInsuranceCommand { get; }

    // Properties for enabling/disabling CoverageOptions listview
    public bool IsCoverageOptionsEnabled => AvailableCoverageOptions?.Any() == true;

    private bool IsPrivateInsuranceType(InsuranceType type)
    {
        return type == InsuranceType.ChildAccidentAndHealthInsurance
               || type == InsuranceType.AdultAccidentAndHealthInsurance
               || type == InsuranceType.AdultLifeInsurance;
    }

    private async Task OnSelectedInsuranceTypeChangedAsync()
    {
        // Update Addon visibility based on selected insurance type
        IsAddonVisible = SelectedInsuranceType == InsuranceType.ChildAccidentAndHealthInsurance ||
                         SelectedInsuranceType == InsuranceType.AdultAccidentAndHealthInsurance;

        SelectedLongTermSicknessAddon = null;
        SelectedSicknessAccidentAddon = null;
        SelectedCoverageOption = null;
        // Run methods to load coverage options and addons
        await LoadCoverageOptions();
        await LoadAddonOptions();
    }

    private async Task LoadCoverageOptions()
    {
        if (SelectedInsuranceType != null)
        {
            var (options, message) = await _insuranceListingController.GetSpecificPrivateOption(SelectedInsuranceType);

            // Add AvailableCoverageOptions to the list
            AvailableCoverageOptions = options;

            // Tell the UI that AvailableCoverageOptions has been updated
            OnPropertyChanged(nameof(AvailableCoverageOptions));
            OnPropertyChanged(nameof(IsCoverageOptionsEnabled));
            OnPropertyChanged(nameof(DisplayYearText));
        }
    }

    private async Task LoadAddonOptions()
    {
        if (SelectedInsuranceType == InsuranceType.ChildAccidentAndHealthInsurance ||
            SelectedInsuranceType == InsuranceType.AdultAccidentAndHealthInsurance)
        {
            var (success, addons, message) = await _insuranceListingController.GetAllInsuranceAddonTypes();

            if (success)
            {
                // Filter out the addons based on the type
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
        var startDate = StartDate ?? DateTime.Now; // Use the chosen start date or today as default
        var endDate = EndDate ?? startDate.AddYears(1); // Use the chosen end date or start date + 1 year as default
        var addons = new List<InsuranceAddonType> { SelectedSicknessAccidentAddon, SelectedLongTermSicknessAddon }
            .Where(addon => addon != null).ToList();
        var insuredPerson = !IsInsuredPersonSameAsPolicyHolder
            ? new InsuredPerson
                { InsuredPersonName = InsuredPersonName, InsuredPersonPersonalNumber = InsuredPersonPersonalNumber }
            : null;

        // Call the controller method
        var result = await _insuranceCreateController.CreatePrivateInsurance(privateCustomer, insuranceType,
            privateCoverageOption, seller, isPolicyHolderInsured, note, paymentPlan, startDate, endDate, addons,
            insuredPerson);

        if (result.success)
            MessageBox.Show($"{result.message}");
        else
            MessageBox.Show($"{result.message}");
    }

    // Check if insurance can be created
    private bool CanCreateInsurance()
    {
        return SelectedInsuranceType != null
               && SelectedCoverageOption != null
               && SelectedPaymentPlan != null;
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
                InsuredPersonPersonalNumber =
                    _policyHolderService.InsurancePolicyHolder.PrivateCustomer?.PersonalNumber;
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
}