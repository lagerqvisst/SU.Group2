using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SU.Backend.Controllers;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Statistics;
using SU.Frontend.Helper;

namespace SU.Frontend.ViewModels.Statistics;

public class TablePageViewModel : ObservableObject
{
    // Controller
    private readonly StatisticsController _statisticsController;

    private InsuranceCategory _selectedInsuranceCategory;

    // Constructor
    public TablePageViewModel(StatisticsController statisticsController)
    {
        _statisticsController = statisticsController;
        PrivateInsuranceStatistics = new ObservableCollection<SellerStatistics>();
        CompanyInsuranceStatistics = new ObservableCollection<SellerStatistics>();

        // Initialize the command
        ExportSellerStatisticsCommand = new RelayCommand(ExecuteExportSellerStatistics);

        // Set default category
        SelectedInsuranceCategory = InsuranceCategory.Private;

        OnInitialized();
    }

    public ObservableCollection<SellerStatistics> PrivateInsuranceStatistics { get; set; }
    public ObservableCollection<SellerStatistics> CompanyInsuranceStatistics { get; set; }

    public ObservableCollection<InsuranceCategory> InsuranceCategories { get; } =
        new() { InsuranceCategory.Private, InsuranceCategory.Company };

    public InsuranceCategory SelectedInsuranceCategory
    {
        get => _selectedInsuranceCategory;
        set
        {
            _selectedInsuranceCategory = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsPrivateInsuranceVisible));
            OnPropertyChanged(nameof(IsCompanyInsuranceVisible));
        }
    }

    // Boolean properties to determine visibility of the data grid
    public bool IsPrivateInsuranceVisible => SelectedInsuranceCategory == InsuranceCategory.Private;
    public bool IsCompanyInsuranceVisible => SelectedInsuranceCategory == InsuranceCategory.Company;

    // Command for exporting statistics
    public ICommand ExportSellerStatisticsCommand { get; }

    private async Task OnInitialized()
    {
        await LoadStatistics();
    }

    // Method to load statistics
    private async Task LoadStatistics()
    {
        var year = DateTime.Now.Year;

        var privateInsuranceTypes = new List<InsuranceType>
        {
            InsuranceType.ChildAccidentAndHealthInsurance,
            InsuranceType.AdultAccidentAndHealthInsurance,
            InsuranceType.AdultLifeInsurance
        };

        var companyInsuranceTypes = new List<InsuranceType>
        {
            InsuranceType.PropertyAndInventoryInsurance,
            InsuranceType.VehicleInsurance,
            InsuranceType.LiabilityInsurance
        };

        var (successPrivate, messagePrivate, privateStatistics) =
            await _statisticsController.GetSellerStatistics(year, privateInsuranceTypes);
        if (successPrivate)
        {
            PrivateInsuranceStatistics.Clear();
            foreach (var stat in privateStatistics) PrivateInsuranceStatistics.Add(stat);
        }
        else
        {
            MessageBox.Show(messagePrivate, "Error fetching statistics", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        var (successCompany, messageCompany, companyStatistics) =
            await _statisticsController.GetSellerStatistics(year, companyInsuranceTypes);
        if (successCompany)
        {
            CompanyInsuranceStatistics.Clear();
            foreach (var stat in companyStatistics) CompanyInsuranceStatistics.Add(stat);
        }
        else
        {
            MessageBox.Show(messageCompany, "Error fetching statistics", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void ExecuteExportSellerStatistics()
    {
        // Determine which statistics to export based on selected category
        var isPrivateInsurance = SelectedInsuranceCategory == InsuranceCategory.Private;
        var statistics = isPrivateInsurance ? PrivateInsuranceStatistics.ToList() : CompanyInsuranceStatistics.ToList();

        // Call export service
        var (success, message) = await _statisticsController.ExportTable(statistics, isPrivateInsurance);
        if (!success)
            MessageBox.Show(message, "Error exporting to Excel", MessageBoxButton.OK, MessageBoxImage.Error);
        else
            MessageBox.Show(message, "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}