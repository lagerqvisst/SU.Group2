using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using SU.Backend.Controllers;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Statistics;
using SU.Frontend.Helper;

namespace SU.Frontend.ViewModels.Statistics;

public class BarChartViewModel : ObservableObject
{
    // Controllers
    private readonly EmployeeController _employeeController;
    private readonly StatisticsController _statisticsController;

    private Employee _selectedSeller;

    // Command
    public ICommand ExportBarChart { get; }

    // Constructor
    public BarChartViewModel(EmployeeController employeeController, StatisticsController statisticsController)
    {
        _statisticsController = statisticsController;
        _employeeController = employeeController;
        Series = new ObservableCollection<ISeries>();

        OnInitialized();

        ExportBarChart = new RelayCommand(async () => await ExportDataAsync(), CanExportData);
    }

    public ObservableCollection<ISeries> Series { get; set; }
    public Axis[] XAxes { get; set; }
    public Axis[] YAxes { get; set; }

    public ObservableCollection<Employee> Sellers { get; set; } = new();

    public Employee SelectedSeller
    {
        get => _selectedSeller;
        set
        {
            _selectedSeller = value;
            OnPropertyChanged();
            if (_selectedSeller != null) LoadDataAsync(2024, _selectedSeller); // Ladda data när en säljare väljs
        }
    }

    private async Task OnInitialized()
    {
        await LoadAllSellers();
    }

    private async Task LoadDataAsync(int year, Employee seller)
    {
        // Get sales statistics for the selected seller and year
        var (success, message, statistics) = await _statisticsController.SellerStatisticsBySeller(year, seller);


        if (!success || statistics == null || statistics.MonthlySales == null) return;

        // Extract total sales per month
        var monthlySales = statistics.MonthlySales.Select(ms => ms.TotalSales).ToArray();

        // Calculate 3-month moving average for trend line
        var trendLine = SellerStatistics.CalculateMovingAverage(monthlySales, 3);

        Series.Clear();

        // Add column series for monthly sales
        Series.Add(new ColumnSeries<int>
        {
            Values = monthlySales,
            Name = "Monthly Sales"
        });

        // Add line series for trend line
        Series.Add(new LineSeries<double>
        {
            Values = trendLine,
            Name = "3M Moving Average",
            LineSmoothness = 0.5
        });

        // Set up axes
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

    // Method to load all sellers
    private async Task LoadAllSellers()
    {
        var result = await _employeeController.GetAllSellers();
        if (result.success)
        {
            Sellers.Clear();
            foreach (var seller in result.salesEmployees) Sellers.Add(seller);
        }
        else
        {
            Console.WriteLine(result.message ?? "Error loading sellers.");
        }
    }

    private bool CanExportData()
    {
        if (_selectedSeller == null)
            return false;
        return true;
    }

    // Method to export data to Excel
    private async Task ExportDataAsync()
    {
        if (_selectedSeller == null)
        {
            // Show a message if no seller is selected
            Console.WriteLine("Please select a seller before exporting data.");
            return;
        }

        var year = 2024;
        var (success, message, statistics) =
            await _statisticsController.SellerStatisticsBySeller(year, _selectedSeller);

        if (success && statistics != null)
        {
            var exportResult = await _statisticsController.ExportBarChart(statistics);
            if (!exportResult.success)
                MessageBox.Show(exportResult.message, "Error exporting to Excel", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            else
                MessageBox.Show(exportResult.message, "Export Successful", MessageBoxButton.OK,
                    MessageBoxImage.Information);
        }
    }
}