using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Statistics;
using SU.Backend.Services.Interfaces;
using SU.Frontend.Helper;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Data;
using LiveChartsCore.SkiaSharpView.Painting;
using SU.Frontend.Views;


public class StatisticsViewModel : ObservableObject
{
    private readonly IStatisticsService _statisticsService;

    // Properties for binding
    public ObservableCollection<SellerStatistics> Stats { get; set; }
    public ObservableCollection<string> InsuranceCategories { get; set; }
    private string _selectedInsuranceCategory;
    public string SelectedInsuranceCategory
    {
        get => _selectedInsuranceCategory;
        set
        {
            _selectedInsuranceCategory = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<ISeries> Series { get; set; }
    public ICartesianAxis[] XAxes { get; set; }
    public ICartesianAxis[] YAxes { get; set; }
    public ICommand LoadStatisticsCommand { get; }

    public StatisticsViewModel(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
        LoadStatisticsCommand = new RelayCommand(async () => await LoadStatistics());
        Stats = new ObservableCollection<SellerStatistics>();
        Series = new ObservableCollection<ISeries>();

        // Initialize insurance categories
        InsuranceCategories = new ObservableCollection<string> { "Privatförsäkring", "Företagsförsäkring" };
        SelectedInsuranceCategory = InsuranceCategories.First();

        // Set up chart axes
        XAxes = new[]
        {
            new Axis
            {
                Labels = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" },
                Name = "Months",
            }
        };
        YAxes = new[]
        {
            new Axis
            {
                Name = "# Sales",
                Labeler = value => value.ToString("N0"),
                MinLimit = -1
            }
        };
    }

    // Legend customization properties
    public SolidColorPaint LegendBackgroundPaint { get; set; }
    public SolidColorPaint LegendTextPaint { get; set; }

    private async Task LoadStatistics()
    {
        var insuranceTypes = GetInsuranceTypes(SelectedInsuranceCategory);
        var statistics = await _statisticsService.GetSellerStatistics(2024, insuranceTypes);

        Stats.Clear();
        foreach (var stat in statistics)
        {
            Stats.Add(stat);
        }

        // Dynamically add columns to the DataGrid in the view
        Application.Current.Dispatcher.Invoke(() =>
        {
            var window = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is Statistics);
            if (window != null)
            {
                var dataGrid = window.FindName("StatisticsDataGrid") as DataGrid;
                if (dataGrid != null)
                {
                    dataGrid.Columns.Clear();

                    // Add static column for Seller Name
                    dataGrid.Columns.Add(new DataGridTextColumn { Header = "Säljare", Binding = new Binding("SellerName") });

                    // Add columns for each month
                    foreach (var month in Enumerable.Range(1, 12))
                    {
                        // Add sub-columns for each insurance type
                        foreach (var insuranceType in insuranceTypes)
                        {
                            dataGrid.Columns.Add(new DataGridTextColumn
                            {
                                Header = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {insuranceType}",
                                Binding = new Binding($"MonthlySales[{month - 1}].InsuranceSalesCounts[{insuranceType}]")
                            });
                        }

                        // Add total column for the month
                        dataGrid.Columns.Add(new DataGridTextColumn
                        {
                            Header = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} Totalt",
                            Binding = new Binding($"MonthlySales[{month - 1}].TotalSales")
                        });
                    }

                    // Add columns for yearly total and average
                    dataGrid.Columns.Add(new DataGridTextColumn { Header = "Totalt", Binding = new Binding("TotalYearlySales") });
                    dataGrid.Columns.Add(new DataGridTextColumn { Header = "Medel/mån", Binding = new Binding("AverageMonthlySales") });
                }
            }
        });

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


    private List<InsuranceType> GetInsuranceTypes(string category)
    {
        return category switch
        {
            "Privatförsäkring" => new List<InsuranceType>
            {
                InsuranceType.ChildAccidentAndHealthInsurance,
                InsuranceType.AdultAccidentAndHealthInsurance,
                InsuranceType.AdultLifeInsurance
            },
            "Företagsförsäkring" => new List<InsuranceType>
            {
                InsuranceType.PropertyAndInventoryInsurance,
                InsuranceType.VehicleInsurance,
                InsuranceType.LiabilityInsurance
            },
            _ => new List<InsuranceType>()
        };
    }
}
