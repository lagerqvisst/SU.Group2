﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using SU.Backend.Controllers;
using SU.Backend.Helper;
using SU.Backend.Models.Enums.Insurance;
using SU.Frontend.Helper;

public class LineChartViewModel : ObservableObject
{
    // Controller
    private readonly StatisticsController _statisticsController;

    public List<InsuranceType> insuranceTypes = EnumService.InsuranceType();

    // Constructor
    public LineChartViewModel(StatisticsController statisticsController)
    {
        _statisticsController = statisticsController;
        Series = new ObservableCollection<ISeries>();

        ExportLineChartCommand = new RelayCommand(async () => await ExportLineChartDataAsync());
        OnInitalized();
    }

    //Command
    public ICommand ExportLineChartCommand { get; }

    public ObservableCollection<ISeries> Series { get; set; }
    public Axis[] XAxes { get; set; }
    public Axis[] YAxes { get; set; }

    private async Task OnInitalized()
    {
        await LoadDataAsync(2024);
    }

    // Method to load data for the line chart
    private async Task LoadDataAsync(int year)
    {
        var (success, message, statistics) =
            await _statisticsController.GetActiveSellerStatistics(year, insuranceTypes);

        if (!success) return;

        foreach (var seller in statistics)
        {
            var monthlySales = seller.MonthlySales.Select(ms => ms.TotalSales).ToArray();

            Series.Add(new LineSeries<int>
            {
                Values = monthlySales,
                Name = seller.SellerName
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
                Labeler = value => value.ToString("N0")
            }
        };

        OnPropertyChanged(nameof(Series));
        OnPropertyChanged(nameof(XAxes));
        OnPropertyChanged(nameof(YAxes));
    }

    // Method to export line chart data to Excel
    private async Task ExportLineChartDataAsync()
    {
        var year = 2024;
        var (success, message, statistics) =
            await _statisticsController.GetActiveSellerStatistics(year, insuranceTypes);

        if (success && statistics != null)
        {
            var exportResult = await _statisticsController.ExportLineChart(statistics);
            if (!exportResult.success)
                MessageBox.Show(exportResult.message, "Error exporting to Excel", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            else
                MessageBox.Show(exportResult.message, "Export Successful", MessageBoxButton.OK,
                    MessageBoxImage.Information);
        }
    }
}