using SU.Backend.Controllers;
using SU.Backend.Models.Invoices;
using SU.Frontend.Helper;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.FinancialAssistantViewModels
{
    public class InvoiceViewModel : ObservableObject
    {
        private readonly InvoiceController _invoiceController;
        public ICommand ExportInvoices { get; set; }

        // Ändra från fält till property
        private ObservableCollection<InvoiceEntry> _invoices = new ObservableCollection<InvoiceEntry>();
        public ObservableCollection<InvoiceEntry> Invoices
        {
            get => _invoices;
            set
            {
                _invoices = value;
                OnPropertyChanged(nameof(Invoices));
            }
        }

        public InvoiceViewModel(InvoiceController invoiceController)
        {
            _invoiceController = invoiceController;
            ExportInvoices = new RelayCommand(ExportToExcel);
            LoadInvoices();
        }

        public async Task LoadInvoices()
        {
            Invoices.Clear();
            var result = await _invoiceController.GenerateInvoiceData();

            if (result.success)
            {
                foreach (var invoice in result.invoiceData)
                {
                    Invoices.Add(invoice);
                }
            }
            else
            {
                Console.WriteLine(result.message ?? "Error loading invoices");
            }
        }

        private async void ExportToExcel()
        {
            var listInvoices = new List<InvoiceEntry>(Invoices);

            var result = await _invoiceController.ExportInvoicesToExcel(listInvoices);

            if (result.Success)
            {
                MessageBox.Show(
                   result.Message,
                   "Export Successful",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information
               );
            }
            else
            {
                MessageBox.Show(
                    result.Message,
                    "Export Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                ); 
            }
        }
    }
}
