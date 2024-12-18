﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SU.Backend.Controllers;
using SU.Backend.Helper;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances;
using SU.Frontend.Helper;
using SU.Frontend.Helper.DI_Objects.InsuranceObjects;
using SU.Frontend.Helper.DI_Objects.User;
using SU.Frontend.Helper.Navigation;

namespace SU.Frontend.ViewModels.CommonViewModels.InsurancesRelated;

public class EditDeleteInsuranceViewModel : ObservableObject
{
    private readonly EmployeeController _employeeController;
    private readonly InsuranceCreateController _insuranceCreateController;

    // Controllers
    private readonly InsuranceListingController _insuranceListingController;

    private readonly ILoggedInUserService _loggedInSeller;

    // Services
    private readonly INavigationService _navigationService;
    private readonly IPolicyHolderService _policyHolderService;

    // Chosen insurance
    private Insurance _selectedInsurance;

    private InsuranceStatus _selectedInsuranceStatus;

    private InsuranceType _selectedInsuranceType;

    private PaymentPlan _selectedPaymentPlan;

    private Employee _selectedSeller;

    // Constructor
    public EditDeleteInsuranceViewModel(INavigationService navigationService, IPolicyHolderService policyHolderService,
        ILoggedInUserService loggedInUserService, EmployeeController employeeController,
        InsuranceListingController insuranceListingController, InsuranceCreateController insuranceCreateController)
    {
        _navigationService = navigationService;
        _policyHolderService = policyHolderService;
        _loggedInSeller = loggedInUserService;
        _insuranceListingController = insuranceListingController;
        _insuranceCreateController = insuranceCreateController;
        _employeeController = employeeController;

        _ = InitializeAsync(); // Use discard to explicitly ignore the returned Task

        InsuranceTypes = EnumService.InsuranceType();
        InsuranceStatusOptions = EnumService.InsuranceStatus();

        PaymentPlans = Enum.GetValues(typeof(PaymentPlan))
            .Cast<PaymentPlan>()
            .ToList();

        SaveInsuranceCommand = new RelayCommand(SaveInsurance);
        DeleteInsuranceCommand = new RelayCommand(DeleteInsurance, CanDeleteInsurance);
    }

    // Commands
    public ICommand SaveInsuranceCommand { get; }
    public ICommand DeleteInsuranceCommand { get; }

    //Lists
    public List<InsuranceType> InsuranceTypes { get; set; }
    public List<PaymentPlan> PaymentPlans { get; set; }

    public InsuranceType SelectedInsuranceType
    {
        get => _selectedInsuranceType;
        set
        {
            _selectedInsuranceType = value;
            OnPropertyChanged();
        }
    }

    public PaymentPlan SelectedPaymentPlan
    {
        get => _selectedPaymentPlan;
        set
        {
            _selectedPaymentPlan = value;
            OnPropertyChanged();
            if (SelectedInsurance != null) SelectedInsurance.PaymentPlan = _selectedPaymentPlan;
        }
    }


    // ObservableCollections for insurances
    public ObservableCollection<Insurance> Insurances { get; set; } = new();

    public Insurance SelectedInsurance
    {
        get => _selectedInsurance;
        set
        {
            _selectedInsurance = value;
            OnPropertyChanged();
            OnSelectedInsuranceChanged(); // Update UI
        }
    }

    public List<InsuranceStatus> InsuranceStatusOptions { get; set; }

    public InsuranceStatus SelectedInsuranceStatus
    {
        get => _selectedInsuranceStatus;
        set
        {
            _selectedInsuranceStatus = value;
            OnPropertyChanged();
            if (SelectedInsurance != null) SelectedInsurance.InsuranceStatus = _selectedInsuranceStatus;
        }
    }

    // ObservableCollections for sellers
    public ObservableCollection<Employee> Sellers { get; set; } = new();

    public Employee SelectedSeller
    {
        get => _selectedSeller;
        set
        {
            _selectedSeller = value;
            OnPropertyChanged();
            if (SelectedInsurance != null)
            {
                SelectedInsurance.Seller = _selectedSeller;
                SelectedInsurance.SellerId = _selectedSeller.EmployeeId;
            }
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

    private bool IsPrivateInsuranceType(InsuranceType type)
    {
        return type == InsuranceType.ChildAccidentAndHealthInsurance
               || type == InsuranceType.AdultAccidentAndHealthInsurance
               || type == InsuranceType.AdultLifeInsurance;
    }

    // Check if insurance can be saved
    private async void SaveInsurance()
    {
        if (SelectedInsurance != null)
        {
            var result = await _insuranceCreateController.UpdateInsurance(SelectedInsurance);

            if (result.success)
                MessageBox.Show($"{result.message}");
            else
                MessageBox.Show($"{result.message}");
        }
    }

    // Check if insurance can be deleted
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

        // Continue with deletion if user confirms
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

    // Load all insurances from the database
    private async Task LoadAllInsurancesAsync()
    {
        var insuranceResult = await _insuranceListingController.GetAllInsurances();
        if (insuranceResult.insurances?.Any() ?? false)
        {
            Insurances.Clear();
            foreach (var insurance in insuranceResult.insurances) Insurances.Add(insurance);
        }
        else
        {
            Console.WriteLine("No insurances found");
        }
    }

    private async Task InitializeAsync()
    {
        await LoadInsurancesAsync();
        await LoadSellersAsync();
    }

    private async Task LoadSellersAsync()
    {
        var result = await _employeeController.GetAllSellers();
        if (result.success && result.salesEmployees != null)
        {
            Sellers.Clear();
            foreach (var seller in result.salesEmployees) Sellers.Add(seller);
        }
        else
        {
            Console.WriteLine("Failed to load sellers: " + result.message);
        }
    }

    // Update UI when insurance is selected
    private void OnSelectedInsuranceChanged()
    {
        if (SelectedInsurance != null)
        {
            SelectedPaymentPlan = SelectedInsurance.PaymentPlan;
            SelectedInsuranceStatus = SelectedInsurance.InsuranceStatus;
            SelectedSeller = SelectedInsurance.Seller;
        }
    }
}