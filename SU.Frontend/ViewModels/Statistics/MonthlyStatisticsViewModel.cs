using System.Collections.ObjectModel;
using SU.Backend.Controllers;
using SU.Backend.Models.Statistics;
using SU.Frontend.Helper;

namespace SU.Frontend.ViewModels.Statistics;

public class MonthlyStatisticsViewModel : ObservableObject
{
    // Controller
    private readonly StatisticsController _statisticsController;

    // Constructor
    public MonthlyStatisticsViewModel(StatisticsController statisticsController)
    {
        _statisticsController = statisticsController;
        InsuranceStatistics = new ObservableCollection<InsuranceStatistics>();

        // Load the statistics when the ViewModel is initialized
        LoadInsuranceStatistics();
    }

    // List of insurance statistics
    public ObservableCollection<InsuranceStatistics> InsuranceStatistics { get; set; }

    // Method to load insurance statistics
    private async void LoadInsuranceStatistics()
    {
        // Call on the method that returns (List<InsuranceStatistics>, string message)
        var result = await _statisticsController.GetMonthlyInsuranceStats();

        if (result.Item1 != null && result.Item1.Any())
        {
            InsuranceStatistics.Clear();
            foreach (var stat in result.Item1) InsuranceStatistics.Add(stat);
        }
        else
        {
            // Show the message when no statistics are found
            Console.WriteLine(result.message ?? "Fel vid hämtning av försäkringsstatistik.");
        }
    }
}