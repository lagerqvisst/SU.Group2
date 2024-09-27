using SU.Backend.Services.Interfaces;
using SU.Frontend.Helper;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Series;
using System.Windows;

namespace SU.Frontend.ViewModels
{
    public class StatisticsViewModel : ObservableObject
    {
        private readonly IStatisticsService _statisticsService;

        // The plot model for OxyPlot
        public PlotModel MyModel { get; private set; }

        // Property for X-axis labels
        public ObservableCollection<string> XAxisLabels { get; set; } = new ObservableCollection<string>
        {
            "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
        };

        // Command to load statistics
        public ICommand LoadStatisticsCommand { get; }

        public StatisticsViewModel(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
            MyModel = new PlotModel { Title = "Seller Statistics" };
            LoadStatisticsCommand = new RelayCommand(async () => await LoadStatistics());
        }

        private async Task LoadStatistics()
        {
            // Clear existing series
            MyModel.Series.Clear();

            try
            {
                // Fetch seller statistics (replace with actual year or user input)
                var statistics = await _statisticsService.GetSellerStatistics(2024);

                // Check if statistics are retrieved
                if (statistics == null || !statistics.Any())
                {
                    // Handle no data case
                    MyModel.InvalidatePlot(true); // Refresh the plot
                    return;
                }

                foreach (var seller in statistics)
                {
                    // Ensure MonthlySales is not null or empty
                    if (seller.MonthlySales == null || !seller.MonthlySales.Any())
                    {
                        continue; // Skip if no sales data
                    }

                    // Create a new series for each seller
                    var lineSeries = new LineSeries
                    {
                        Title = seller.SellerName,
                        ItemsSource = seller.MonthlySales.Select(m => new DataPoint(m.Month, m.TotalSales)).ToList()
                    };

                    // Add the series to the model
                    MyModel.Series.Add(lineSeries);
                }

                // Notify that the chart data has been updated
                OnPropertyChanged(nameof(MyModel));
            }
            catch (Exception ex)
            {
                // Handle any exceptions, such as logging or displaying an error message
                MessageBox.Show($"Error loading statistics: {ex.Message}");
            }
            finally
            {
                MyModel.InvalidatePlot(true); // Refresh the plot after loading data
            }
        }
    }
}
