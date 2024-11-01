using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WPF;
using SU.Backend.Helper;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Services.Interfaces;
using SU.Frontend.Helper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SU.Frontend.ViewModels.Statistics
{
    public class LineChartViewModel : ObservableObject
    {
        private readonly IStatisticsService _statisticsService;
        public ObservableCollection<ISeries> Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        public List<InsuranceType> insuranceTypes = EnumService.InsuranceType();

        public LineChartViewModel(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
            Series = new ObservableCollection<ISeries>();

            OnInitalized();
        }

        private async Task OnInitalized()
        {
            await LoadDataAsync(2024);
        }

        private async Task LoadDataAsync(int year)
        {
            var (success, message, statistics) = await _statisticsService.GetActiveSellerStatistics(year, insuranceTypes);

            if (!success) return;

            // Skapa en linje för varje säljare baserat på deras månatliga försäljning
            foreach (var seller in statistics)
            {
                var monthlySales = seller.MonthlySales.Select(ms => ms.TotalSales).ToArray();

                Series.Add(new LineSeries<int>
                {
                    Values = monthlySales,
                    Name = seller.SellerName, // Detta visas i legenden
                });



            }

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
                    Labeler = value => value.ToString("N0") // Anpassa etiketten för Y-axeln
                }
            };

            OnPropertyChanged(nameof(Series));
            OnPropertyChanged(nameof(XAxes));
            OnPropertyChanged(nameof(YAxes));
        }
    }
}
