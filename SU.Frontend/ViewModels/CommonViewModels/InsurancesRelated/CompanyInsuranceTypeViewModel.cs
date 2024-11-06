using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SU.Backend.Controllers;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances.Coverage;
using SU.Frontend.Helper;
using SU.Frontend.Helper.DI_Objects.InsuranceObjects;
using SU.Frontend.Helper.DI_Objects.User;
using SU.Frontend.Helper.Navigation;

public class CompanyInsuranceTypeViewModel : ObservableObject
{
    private readonly InsuranceCreateController _insuranceCreateController;

    // Controllers for handling insurance creation and listing
    private readonly InsuranceListingController _insuranceListingController;

    private readonly ILoggedInUserService _loggedInSeller;

    // Services
    private readonly IPolicyHolderService _policyHolderService;

    private InsuranceType _selectedInsuranceType;

    private PaymentPlan _selectedPaymentPlan;

    // Constructor
    public CompanyInsuranceTypeViewModel(ILoggedInUserService loggedInUserService,
        IPolicyHolderService policyHolderService, InsuranceCreateController insuranceCreateController,
        InsuranceListingController insuranceListingController)
    {
        _policyHolderService = policyHolderService;
        _loggedInSeller = loggedInUserService;
        _insuranceCreateController = insuranceCreateController;
        _insuranceListingController = insuranceListingController;

        CompanyInsuranceTypes = Enum.GetValues(typeof(InsuranceType))
            .Cast<InsuranceType>()
            .Where(t => IsCompanyInsuranceType(t))
            .ToList();

        PaymentPlans = Enum.GetValues(typeof(PaymentPlan)).Cast<PaymentPlan>().ToList();
        SelectedPaymentPlan = PaymentPlan.Monthly;

        CreateInsuranceCommand = new RelayCommand(CreateInsurance, CanCreateInsurance);

        // Sätt standardvärden för start- och slutdatum
        StartDate = DateTime.Now;
        EndDate = StartDate.Value.AddYears(1);

    }

    // Lists
    public List<InsuranceType> CompanyInsuranceTypes { get; set; }
    public List<PaymentPlan> PaymentPlans { get; set; }

    // ObservableCollections
    public ObservableCollection<LiabilityCoverageOption> LiabilityCoverageOptions { get; } = new();
    public ObservableCollection<VehicleInsuranceOption> VehicleInsuranceOptions { get; } = new();
    public ObservableCollection<RiskZone> RiskZones { get; } = new();

    public InsuranceType SelectedInsuranceType
    {
        get => _selectedInsuranceType;
        set
        {
            _selectedInsuranceType = value;
            OnPropertyChanged();
            _ = LoadCoverageOptionsAsync();
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

    // Command for creating insurance
    public ICommand CreateInsuranceCommand { get; }

    // Method to check if the insurance type is a company insurance type
    private bool IsCompanyInsuranceType(InsuranceType type)
    {
        return type == InsuranceType.VehicleInsurance
               || type == InsuranceType.LiabilityInsurance
               || type == InsuranceType.PropertyAndInventoryInsurance;
    }

    // Method to load coverage options based on the selected insurance type
    private async Task LoadCoverageOptionsAsync()
    {
        // Återställ synlighet för alla sektioner
        IsLiabilityCoverageVisible = false;
        IsVehicleInsuranceVisible = false;
        IsPropertyCoverageVisible = false;

        if (SelectedInsuranceType == InsuranceType.LiabilityInsurance)
        {
            IsLiabilityCoverageVisible = true;
            var (options, message) = await _insuranceListingController.GetAllLiabilityCoverageOptions();
            LiabilityCoverageOptions.Clear();
            foreach (var option in options)
                LiabilityCoverageOptions.Add(option);
        }
        else if (SelectedInsuranceType == InsuranceType.VehicleInsurance)
        {
            IsVehicleInsuranceVisible = true;
            var (vehicleOptions, vehicleMessage) = await _insuranceListingController.GetAllVehicleInsuranceOptions();
            VehicleInsuranceOptions.Clear();
            foreach (var option in vehicleOptions)
                VehicleInsuranceOptions.Add(option);

            var (riskZones, riskZoneMessage) = await _insuranceListingController.GetAllRiskZones();
            RiskZones.Clear();
            foreach (var riskZone in riskZones)
                RiskZones.Add(riskZone);
        }
        else if (SelectedInsuranceType == InsuranceType.PropertyAndInventoryInsurance)
        {
            IsPropertyCoverageVisible = true;
        }

        // Update visibility properties for the UI
        OnPropertyChanged(nameof(IsLiabilityCoverageVisible));
        OnPropertyChanged(nameof(IsVehicleInsuranceVisible));
        OnPropertyChanged(nameof(IsPropertyCoverageVisible));
    }

    // Method to create insurance based on the selected insurance type
    private async void CreateInsurance()
    {
        var insuranceType = SelectedInsuranceType;
        var note = Note;
        var paymentPlan = SelectedPaymentPlan;
        var seller = _loggedInSeller.LoggedInEmployee;

        if (insuranceType == InsuranceType.PropertyAndInventoryInsurance)
        {
            // Property and Inventory Insurance
            var companyCustomer = _policyHolderService.InsurancePolicyHolder.CompanyCustomer;
            var propertyCoverage = new PropertyAndInventoryCoverage(PropertyValue, InventoryValue)
            {
                PropertyAddress = PropertyAddress
            };

            // Call CreatePropertyInventoryInsurance in the controller
            var result = await _insuranceCreateController.CreatePropertyInventoryInsurance(companyCustomer,
                propertyCoverage, seller, note, paymentPlan);
            ShowMessage(result);
        }
        else if (insuranceType == InsuranceType.LiabilityInsurance)
        {
            // Liability Insurance
            var companyCustomer = _policyHolderService.InsurancePolicyHolder.CompanyCustomer;
            var liabilityCoverage = new LiabilityCoverage
            {
                LiabilityCoverageOption = SelectedLiabilityCoverageOption
            };

            // Call CreateLiabilityInsurance in the controller
            var result = await _insuranceCreateController.CreateLiabilityInsurance(companyCustomer, liabilityCoverage,
                seller, note, paymentPlan);
            ShowMessage(result);
        }
        else if (insuranceType == InsuranceType.VehicleInsurance)
        {
            // Vehicle Insurance
            var companyCustomer = _policyHolderService.InsurancePolicyHolder.CompanyCustomer;
            var vehicleCoverage = new VehicleInsuranceCoverage
            {
                VehicleInsuranceOption = SelectedVehicleCoverageOption,
                RiskZone = SelectedRiskZone
            };

            // Call CreateVehicleInsurance in the controller
            var result = await _insuranceCreateController.CreateVehicleInsurance(companyCustomer, vehicleCoverage,
                SelectedRiskZone, seller, note, paymentPlan);
            ShowMessage(result);
        }
    }

    // Method to show a message box with the result of the insurance creation
    private void ShowMessage((bool success, string message) result)
    {
        MessageBox.Show(result.message, result.success ? "Success" : "Error", MessageBoxButton.OK,
            MessageBoxImage.Information);
    }

    // Method to check if the insurance can be created
    private bool CanCreateInsurance()
    {
        return SelectedInsuranceType != null && SelectedPaymentPlan != null && StartDate.HasValue && EndDate.HasValue;
    }

    #region Properties

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

    private RiskZone _selectedRiskZone;

    public RiskZone SelectedRiskZone
    {
        get => _selectedRiskZone;
        set
        {
            _selectedRiskZone = value;
            OnPropertyChanged();
        }
    }

    // Properties for Property and Inventory Coverage
    private string _propertyAddress;

    public string PropertyAddress
    {
        get => _propertyAddress;
        set
        {
            _propertyAddress = value;
            OnPropertyChanged();
        }
    }

    private decimal _propertyValue;

    public decimal PropertyValue
    {
        get => _propertyValue;
        set
        {
            _propertyValue = value;
            OnPropertyChanged();
        }
    }

    private decimal _inventoryValue;

    public decimal InventoryValue
    {
        get => _inventoryValue;
        set
        {
            _inventoryValue = value;
            OnPropertyChanged();
        }
    }

    private bool _isLiabilityCoverageVisible;

    public bool IsLiabilityCoverageVisible
    {
        get => _isLiabilityCoverageVisible;
        set
        {
            _isLiabilityCoverageVisible = value;
            OnPropertyChanged();
        }
    }

    private bool _isVehicleInsuranceVisible;

    public bool IsVehicleInsuranceVisible
    {
        get => _isVehicleInsuranceVisible;
        set
        {
            _isVehicleInsuranceVisible = value;
            OnPropertyChanged();
        }
    }

    private bool _isPropertyCoverageVisible;

    public bool IsPropertyCoverageVisible
    {
        get => _isPropertyCoverageVisible;
        set
        {
            _isPropertyCoverageVisible = value;
            OnPropertyChanged();
        }
    }

    // Properties for Liability Coverage
    private LiabilityCoverageOption _selectedLiabilityCoverageOption;

    public LiabilityCoverageOption SelectedLiabilityCoverageOption
    {
        get => _selectedLiabilityCoverageOption;
        set
        {
            _selectedLiabilityCoverageOption = value;
            OnPropertyChanged();
        }
    }

    // Properties for Vehicle Insurance Option
    private VehicleInsuranceOption _selectedVehicleCoverageOption;

    public VehicleInsuranceOption SelectedVehicleCoverageOption
    {
        get => _selectedVehicleCoverageOption;
        set
        {
            _selectedVehicleCoverageOption = value;
            OnPropertyChanged();
        }
    }

    #endregion Properties
}