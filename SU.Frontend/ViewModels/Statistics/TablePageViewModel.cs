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
    public class TablePageViewModel : ObservableObject
    {
        private readonly IStatisticsService _statisticsService;

        public ObservableCollection<SellerStatistics> PrivateInsuranceStatistics { get; set; }
        public ObservableCollection<SellerStatistics> CompanyInsuranceStatistics { get; set; }

        public TablePageViewModel(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
            PrivateInsuranceStatistics = new ObservableCollection<SellerStatistics>();
            CompanyInsuranceStatistics = new ObservableCollection<SellerStatistics>();

            LoadStatistics();
        }

        private async void LoadStatistics()
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
                MessageBox.Show(messagePrivate, "Fel vid hämtning av statistik", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(messageCompany, "Fel vid hämtning av statistik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }



}
