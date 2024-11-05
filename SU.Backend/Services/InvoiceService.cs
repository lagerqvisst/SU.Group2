using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Helper;
using SU.Backend.Models.Invoices;
using SU.Backend.Services.Interfaces;

namespace SU.Backend.Services;

/// <summary>
///     This class is responsible for handling the business logic for invoice generation.
/// </summary>
public class InvoiceService : IInvoiceService
{
    private readonly ILogger<InvoiceService> _logger;
    private readonly UnitOfWork _unitOfWork;

    public InvoiceService(ILogger<InvoiceService> logger, UnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    ///     Method to generate invoice data for all active insurances that should be invoiced this month.
    ///     Uses helper functions to determine which insurances to invoice and to create the invoice data.
    /// </summary>
    /// <returns></returns>
    public async Task<(bool success, string message, List<InvoiceEntry> invoiceData)> GenerateInvoiceData()
    {
        _logger.LogInformation("Starting the invoice data generation process...");

        try
        {
            var currentDate = DateTime.Now; // Alltid aktuell månad

            // Hämta aktiva försäkringar som ska faktureras
            var insurancesToInvoice = await _unitOfWork.Insurances.GetInsurancesForInvoicing(currentDate);

            var invoiceEntries = insurancesToInvoice
                .GroupBy(i => i.InsurancePolicyHolder.CompanyCustomer != null
                    ? i.InsurancePolicyHolder.CompanyCustomer
                    : (object)i.InsurancePolicyHolder.PrivateCustomer) // Gruppera per kund
                .Select(g =>
                {
                    var firstInsurance = g.First(); // Hämta den första försäkringen för denna grupp
                    var policyHolder = firstInsurance.InsurancePolicyHolder;
                    var invoiceEntry = InvoiceHelper.CreateInvoiceEntry(policyHolder);

                    // Summera premierna för alla försäkringar i gruppen
                    var totalPremium = g.Sum(i => i.Premium);
                    invoiceEntry.Premium = $"{totalPremium:N0} SEK"; // Format with "SEK" and no decimals

                    return invoiceEntry;
                })
                .ToList();

            if (!invoiceEntries.Any())
            {
                _logger.LogInformation("No insurances to invoice for this month.");
                return (false, "No insurances to invoice for this month.", new List<InvoiceEntry>());
            }

            return (true, "Invoice data generated successfully.", invoiceEntries);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during invoice data generation");
            return (false, "An error occurred during invoice data generation.", new List<InvoiceEntry>());
        }
    }
}