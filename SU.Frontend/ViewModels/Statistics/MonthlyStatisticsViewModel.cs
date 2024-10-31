using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SU.Backend.Controllers;
using SU.Backend.Models.Statistics;
using SU.Frontend.Helper;

namespace SU.Frontend.ViewModels.Statistics
{
    public class MonthlyStatisticsViewModel : ObservableObject
    {
        private readonly StatisticsController _statisticsController;

        // Lista över försäkringsstatistik
        public ObservableCollection<InsuranceStatistics> InsuranceStatistics { get; set; }

        public MonthlyStatisticsViewModel(StatisticsController statisticsController)
        {
            _statisticsController = statisticsController;
            InsuranceStatistics = new ObservableCollection<InsuranceStatistics>();

            // Ladda statistiken när ViewModel initieras
            LoadInsuranceStatistics();
        }

        private async void LoadInsuranceStatistics()
        {
            // Kalla på din metod som returnerar (List<InsuranceStatistics>, string message)
            var result = await _statisticsController.GetMonthlyInsuranceStats();

            if (result.Item1 != null && result.Item1.Any())
            {
                InsuranceStatistics.Clear();
                foreach (var stat in result.Item1)
                {
                    InsuranceStatistics.Add(stat);
                }
            }
            else
            {
                // Hantera meddelandet när ingen statistik finns
                Console.WriteLine(result.message ?? "Fel vid hämtning av försäkringsstatistik.");
            }
        }
    }
}
















