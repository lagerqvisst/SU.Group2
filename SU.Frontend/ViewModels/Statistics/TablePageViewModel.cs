using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Statistics;
using SU.Backend.Services.Interfaces;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SU.Frontend.ViewModels.Statistics
{
    using System.Windows.Input;

    public class TablePageViewModel : ObservableObject
    {
        private readonly IStatisticsService _statisticsService;
        private readonly IDataExportService _dataExportService;

        public ObservableCollection<SellerStatistics> PrivateInsuranceStatistics { get; set; }
        public ObservableCollection<SellerStatistics> CompanyInsuranceStatistics { get; set; }
        public ObservableCollection<InsuranceCategory> InsuranceCategories { get; } =
            new ObservableCollection<InsuranceCategory> { InsuranceCategory.Private, InsuranceCategory.Company };

        private InsuranceCategory _selectedInsuranceCategory;
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

        public bool IsPrivateInsuranceVisible => SelectedInsuranceCategory == InsuranceCategory.Private;
        public bool IsCompanyInsuranceVisible => SelectedInsuranceCategory == InsuranceCategory.Company;

        // Command for exporting statistics
        public ICommand ExportSellerStatisticsCommand { get; }

        public TablePageViewModel(IStatisticsService statisticsService, IDataExportService dataExportService)
        {
            _statisticsService = statisticsService;
            _dataExportService = dataExportService;
            PrivateInsuranceStatistics = new ObservableCollection<SellerStatistics>();
            CompanyInsuranceStatistics = new ObservableCollection<SellerStatistics>();

            // Initialize the command
            ExportSellerStatisticsCommand = new RelayCommand(ExecuteExportSellerStatistics);

            // Set default category
            SelectedInsuranceCategory = InsuranceCategory.Private;

            OnInitialized();
        }

        private async Task OnInitialized()
        {
            await LoadStatistics();
        }

        private async Task LoadStatistics()
        {
            int year = DateTime.Now.Year;

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

            var (successPrivate, messagePrivate, privateStatistics) = await _statisticsService.GetSellerStatistics(year, privateInsuranceTypes);
            if (successPrivate)
            {
                PrivateInsuranceStatistics.Clear();
                foreach (var stat in privateStatistics)
                {
                    PrivateInsuranceStatistics.Add(stat);
                }
            }
            else
            {
                MessageBox.Show(messagePrivate, "Error fetching statistics", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var (successCompany, messageCompany, companyStatistics) = await _statisticsService.GetSellerStatistics(year, companyInsuranceTypes);
            if (successCompany)
            {
                CompanyInsuranceStatistics.Clear();
                foreach (var stat in companyStatistics)
                {
                    CompanyInsuranceStatistics.Add(stat);
                }
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
            var (success, message) = await _dataExportService.ExportSellerStatisticsToExcel(statistics, isPrivateInsurance);
            if (!success)
            {
                MessageBox.Show(message, "Error exporting to Excel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show(message, "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

}
