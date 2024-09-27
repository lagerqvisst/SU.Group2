using SU.Backend.Services.Interfaces;
using SU.Frontend.Helper;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using SU.Backend.Models.Statistics;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using SU.Backend.Models.Enums.Insurance;
using System.Globalization;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;


namespace SU.Frontend.ViewModels
{
    public class StatisticsViewModel : ObservableObject
    {
        private readonly IStatisticsService _statisticsService;

        public ObservableCollection<SellerStatistics> Stats { get; set; }

        public ObservableCollection<ISeries> Series { get; set; }

        public ICartesianAxis[] XAxes { get; set; }
        public ICartesianAxis[] YAxes { get; set; }

        // Legend customization properties
        public SolidColorPaint LegendBackgroundPaint { get; set; }
        public SolidColorPaint LegendTextPaint { get; set; }
        public ICommand LoadStatisticsCommand { get; }

        public StatisticsViewModel(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
            LoadStatisticsCommand = new RelayCommand(async () => await LoadStatistics());
            Stats = new ObservableCollection<SellerStatistics>();
            Series = new ObservableCollection<ISeries>();
            // Set up axis label formatters
            // Set up the X-axis with month labels
            XAxes = new[]
            {
                new Axis
                {
                    Labels = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" },
                    Name = "Months",
                    NamePaint = new SolidColorPaint(SKColors.Black), // Optional: Set the color for the axis name
                }
            };

            // Set up the Y-axis
            YAxes = new[]
            {
                new Axis
                {
                    Name = "# Sales",
                    Labeler = value => value.ToString("N0"), // Format as an integer with no decimals
                    MinLimit = -1
                }
            };

            // Set up legend styling
            LegendBackgroundPaint = new SolidColorPaint(SKColors.LightGray);
            LegendTextPaint = new SolidColorPaint(SKColors.Black);
        }

        private async Task LoadStatistics()
        {
            var statistics = await _statisticsService.GetSellerStatistics(2024);

            Stats.Clear();
            foreach (var stat in statistics)
            {
                Stats.Add(stat);
            }

            Series.Clear();
            foreach (var stat in statistics.Where(stat => stat.MonthlySales.Any(s => s.TotalSales > 0)))
            {
                var monthlySalesValues = Enumerable.Range(1, 12)
                                                   .Select(month => stat.MonthlySales
                                                       .FirstOrDefault(m => m.Month == month)?.TotalSales ?? 0)
                                                   .ToArray();

                var lineSeries = new LineSeries<int>
                {
                    Values = monthlySalesValues,
                    Name = stat.SellerName,
                    Fill = null
                };

                Series.Add(lineSeries);
            }

            OnPropertyChanged(nameof(Series));
        }

    }
}
