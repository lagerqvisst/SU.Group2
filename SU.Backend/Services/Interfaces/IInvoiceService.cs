using SU.Backend.Models.Invoices;

namespace SU.Backend.Services.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the InvoiceService class must implement.
/// </summary>
public interface IInvoiceService
{
    Task<(bool success, string message, List<InvoiceEntry> invoiceData)> GenerateInvoiceData();
}