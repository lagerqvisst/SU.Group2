using System.Collections.ObjectModel;
using SU.Backend.Controllers;
using SU.Backend.Models.Insurances;
using SU.Frontend.Helper;

namespace SU.Frontend.ViewModels.CommonViewModels.InsurancesRelated;

public class ShowInsuranceViewModel : ObservableObject
{
    private readonly InsuranceListingController _insuranceListingController;

    // Chosen insurance
    private Insurance _selectedInsurance;

    private string insurancePolicyHolderName;

    public ShowInsuranceViewModel(InsuranceListingController insuranceListingController)
    {
        _insuranceListingController = insuranceListingController;

        Task.Run(async () => await LoadInsurancesAsync()).Wait();
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
            OnPropertyChanged(nameof(SelectedInsuranceAsCollection)); // Update DataGrid

            UpdateCustomerName(); // Update policy holder name
            OnPropertyChanged(nameof(InsurancePolicyHolder)); // Update UI
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

    public string InsurancePolicyHolder
    {
        get => insurancePolicyHolderName;
        set
        {
            insurancePolicyHolderName = value;
            OnPropertyChanged();
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
            foreach (var insurance in insuranceResult.insurances) Insurances.Add(insurance);
        }
        else
        {
            Console.WriteLine("No insurances found");
        }
    }

    private void UpdateCustomerName()
    {
        if (SelectedInsurance != null)
        {
            var insurancePolicyHolder = SelectedInsurance.InsurancePolicyHolder;

            if (insurancePolicyHolder?.PrivateCustomer != null)
                insurancePolicyHolderName =
                    $"{insurancePolicyHolder.PrivateCustomer.FirstName} {insurancePolicyHolder.PrivateCustomer.LastName}";
            else if (insurancePolicyHolder?.CompanyCustomer != null)
                insurancePolicyHolderName = insurancePolicyHolder.CompanyCustomer.CompanyName;
            else
                insurancePolicyHolderName = "Unknown";
        }
        else
        {
            insurancePolicyHolderName = " ";
        }
    }
}