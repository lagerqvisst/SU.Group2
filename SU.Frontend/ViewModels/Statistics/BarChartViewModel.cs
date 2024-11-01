using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WPF;
using SU.Backend.Controllers;
using SU.Backend.Models.Employees;
using SU.Backend.Services.Interfaces;
using SU.Frontend.Helper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SU.Frontend.ViewModels.Statistics
{
    public class BarChartViewModel : ObservableObject
    {
        private readonly IStatisticsService _statisticsService;
        private readonly EmployeeController _employeeController;

        public ObservableCollection<ISeries> Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        public ObservableCollection<Employee> Sellers { get; set; } = new ObservableCollection<Employee>();
        private Employee _selectedSeller;
        public Employee SelectedSeller
        {
            get => _selectedSeller;
            set
            {
                _selectedSeller = value;
                OnPropertyChanged();
                if (_selectedSeller != null)
                {
                    LoadDataAsync(2024, _selectedSeller); // Ladda data när en säljare väljs
                }
            }
        }

        public BarChartViewModel(IStatisticsService statisticsService, EmployeeController employeeController)
        {
            _statisticsService = statisticsService;
            _employeeController = employeeController;
            Series = new ObservableCollection<ISeries>();

            OnInitialized();
        }

        private async Task OnInitialized()
        {
            await LoadAllSellers();
        }

        private async Task LoadDataAsync(int year, Employee seller)
        {
            // Hämta försäljningsstatistik för den valda säljaren och året
            var (success, message, statistics) = await _statisticsService.GetSellerStatisticsBySeller(year, seller);

            if (!success || statistics == null || statistics.MonthlySales == null) return;

            // Extrahera total försäljning per månad
            var monthlySales = statistics.MonthlySales.Select(ms => ms.TotalSales).ToArray();

            // Beräkna 3-månaders glidande medelvärde för trendlinje
            var trendLine = CalculateMovingAverage(monthlySales, 3);

            Series.Clear();

            // Lägg till stapeldiagram för månatlig försäljning
            Series.Add(new ColumnSeries<int>
            {
                Values = monthlySales,
                Name = "Monthly Sales"
            });

            // Lägg till linjediagram för trendlinje
            Series.Add(new LineSeries<double>
            {
                Values = trendLine,
                Name = "3M Moving Average",
                LineSmoothness = 0.5
            });

            // Sätt upp axlarna
            XAxes = new[]
            {
                new Axis
                {
                    Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
                }
            };

            YAxes = new[]
            {
                new Axis
                {
                    Labeler = value => value.ToString("N0")
                }
            };

            OnPropertyChanged(nameof(Series));
            OnPropertyChanged(nameof(XAxes));
            OnPropertyChanged(nameof(YAxes));
        }

        private async Task LoadAllSellers()
        {
            var result = await _employeeController.GetAllSellers();
            if (result.success)
            {
                Sellers.Clear();
                foreach (var seller in result.salesEmployees)
                {
                    Sellers.Add(seller);
                }
            }
            else
            {
                Console.WriteLine(result.message ?? "Error loading sellers.");
            }
        }

        private static List<double> CalculateMovingAverage(int[] values, int period)
        {
            var movingAverage = new List<double>();
            for (int i = 0; i < values.Length; i++)
            {
                if (i < period - 1)
                {
                    movingAverage.Add(double.NaN);
                }
                else
                {
                    var avg = values.Skip(i - (period - 1)).Take(period).Average();
                    movingAverage.Add(avg);
                }
            }
            return movingAverage;
        }
    }
}
