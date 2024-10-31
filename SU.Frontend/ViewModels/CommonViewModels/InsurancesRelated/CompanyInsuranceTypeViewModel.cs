﻿using SU.Backend.Controllers;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances.Coverage;
using SU.Frontend.Helper.Navigation;
using SU.Frontend.Helper;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using SU.Frontend.Helper.DI_Objects.InsuranceObjects;
using SU.Frontend.Helper.DI_Objects.User;

public class CompanyInsuranceTypeViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IPolicyHolderService _policyHolderService;
    private readonly ILoggedInUserService _loggedInSeller;
    private readonly InsuranceListingController _insuranceListingController;
    private readonly InsuranceCreateController _insuranceCreateController;

    public List<InsuranceType> CompanyInsuranceTypes { get; set; }
    public List<PaymentPlan> PaymentPlans { get; set; }

    private InsuranceType _selectedInsuranceType;
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

    public ObservableCollection<LiabilityCoverageOption> LiabilityCoverageOptions { get; private set; } = new ObservableCollection<LiabilityCoverageOption>();
    public ObservableCollection<VehicleInsuranceOption> VehicleInsuranceOptions { get; private set; } = new ObservableCollection<VehicleInsuranceOption>();
    public ObservableCollection<RiskZone> RiskZones { get; private set; } = new ObservableCollection<RiskZone>();

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

    // Egenskaper för Property and Inventory Coverage
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


    public ICommand CreateInsuranceCommand { get; }


    public CompanyInsuranceTypeViewModel(INavigationService navigationService, ILoggedInUserService loggedInUserService, IPolicyHolderService policyHolderService,InsuranceCreateController insuranceCreateController, InsuranceListingController insuranceListingController)
    {
        _navigationService = navigationService;
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
    }

    private bool IsCompanyInsuranceType(InsuranceType type)
    {
        return type == InsuranceType.VehicleInsurance
            || type == InsuranceType.LiabilityInsurance
            || type == InsuranceType.PropertyAndInventoryInsurance;
    }

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

        // Uppdatera UI med nya synlighetsinställningar
        OnPropertyChanged(nameof(IsLiabilityCoverageVisible));
        OnPropertyChanged(nameof(IsVehicleInsuranceVisible));
        OnPropertyChanged(nameof(IsPropertyCoverageVisible));
    }


    private async void CreateInsurance()
    {
        var insuranceType = SelectedInsuranceType;
        var note = Note;
        var paymentPlan = SelectedPaymentPlan;
        var seller = _loggedInSeller.LoggedInEmployee;

        if (insuranceType == InsuranceType.PropertyAndInventoryInsurance)
        {
            // Egendoms- och inventarieförsäkring
            var companyCustomer = _policyHolderService.InsurancePolicyHolder.CompanyCustomer;
            var propertyCoverage = new PropertyAndInventoryCoverage(PropertyValue, InventoryValue)
            {
                PropertyAddress = PropertyAddress
            };



            // Anropa CreatePropertyInventoryInsurance i kontrollern
            var result = await _insuranceCreateController.CreatePropertyInventoryInsurance(companyCustomer, propertyCoverage, seller, note, paymentPlan);
            ShowMessage(result);
        }
        else if (insuranceType == InsuranceType.LiabilityInsurance)
        {
            // Ansvarsförsäkring
            var companyCustomer = _policyHolderService.InsurancePolicyHolder.CompanyCustomer;
            var liabilityCoverage = new LiabilityCoverage
            {
                LiabilityCoverageOption = SelectedLiabilityCoverageOption
            };

            // Anropa CreateLiabilityInsurance i kontrollern
            var result = await _insuranceCreateController.CreateLiabilityInsurance(companyCustomer, liabilityCoverage, seller, note, paymentPlan);
            ShowMessage(result);
        }
        else if (insuranceType == InsuranceType.VehicleInsurance)
        {
            // Fordonsförsäkring
            var companyCustomer = _policyHolderService.InsurancePolicyHolder.CompanyCustomer;
            var vehicleCoverage = new VehicleInsuranceCoverage
            {
                VehicleInsuranceOption = SelectedVehicleCoverageOption,
                RiskZone = SelectedRiskZone

            };

            // Anropa CreateVehicleInsurance i kontrollern
            var result = await _insuranceCreateController.CreateVehicleInsurance(companyCustomer, vehicleCoverage, SelectedRiskZone, seller, note, paymentPlan);
            ShowMessage(result);
        }
    }

    private void ShowMessage((bool success, string message) result)
    {
        MessageBox.Show(result.message, result.success ? "Success" : "Error", MessageBoxButton.OK, MessageBoxImage.Information);
    }


    private bool CanCreateInsurance()
    {
        return SelectedInsuranceType != null && SelectedPaymentPlan != null && StartDate.HasValue && EndDate.HasValue;
    }
}