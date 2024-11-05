using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SU.Backend.Controllers;
using SU.Backend.Models.Invoices;
using SU.Frontend.Helper;

namespace SU.Frontend.ViewModels.FinancialAssistantViewModels;

public class InvoiceViewModel : ObservableObject
{
    private readonly InvoiceController _invoiceController;

    // Ändra från fält till property
    private ObservableCollection<InvoiceEntry> _invoices = new();

    public InvoiceViewModel(InvoiceController invoiceController)
    {
        _invoiceController = invoiceController;
        ExportInvoices = new RelayCommand(ExportToExcel);
        LoadInvoices();
    }

    public ICommand ExportInvoices { get; set; }

    public ObservableCollection<InvoiceEntry> Invoices
    {
        get => _invoices;
        set
        {
            _invoices = value;
            OnPropertyChanged();
        }
    }

    public async Task LoadInvoices()
    {
        Invoices.Clear();
        var result = await _invoiceController.GenerateInvoiceData();

        if (result.success)
            foreach (var invoice in result.invoiceData)
                Invoices.Add(invoice);
        else
            Console.WriteLine(result.message ?? "Error loading invoices");
    }

    private async void ExportToExcel()
    {
        var listInvoices = new List<InvoiceEntry>(Invoices);

        var result = await _invoiceController.ExportInvoicesToExcel(listInvoices);

        if (result.Success)
            MessageBox.Show(
                result.Message,
                "Export Successful",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        else
            MessageBox.Show(
                result.Message,
                "Export Failed",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
    }
}