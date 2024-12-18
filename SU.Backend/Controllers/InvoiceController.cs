﻿using Microsoft.Extensions.Logging;
using SU.Backend.Models.Invoices;
using SU.Backend.Services.Interfaces;

namespace SU.Backend.Controllers;

/// <summary>
///     This class is responsible for handling the invoice controller.
///     Makes logic available in the Viewmodel
///     More info about the logic for each method can be found in the Service function each controller method uses.
/// </summary>
public class InvoiceController
{
    private readonly IDataExportService _dataExportService;

    // Services
    private readonly IInvoiceService _invoiceService;
    private readonly ILogger<InvoiceController> _logger;

    // Constructor
    public InvoiceController(IInvoiceService invoiceService, IDataExportService dataExportService,
        ILogger<InvoiceController> logger)
    {
        _invoiceService = invoiceService;
        _dataExportService = dataExportService;
        _logger = logger;
    }

    // Controller for GenerateInvoiceData method
    public async Task<(bool success, string message, List<InvoiceEntry> invoiceData)> GenerateInvoiceData()
    {
        _logger.LogInformation("Controller activated to generate invoice data...");

        var result = await _invoiceService.GenerateInvoiceData();

        if (result.success)
        {
            _logger.LogInformation($"Invoice data generated successfully:\n{result.message}");
            return (result.success, result.message, result.invoiceData);
        }

        _logger.LogWarning($"Error generating invoice data: {result.message}");
        return (result.success, result.message, new List<InvoiceEntry>());
    }

    // Controller for ExportInvoicesToExcel method
    public async Task<(bool Success, string Message)> ExportInvoicesToExcel(List<InvoiceEntry> invoices)
    {
        _logger.LogInformation("Exporting invoices to Excel...");

        var result = await _dataExportService.ExportInvoicesToExcel(invoices);

        if (result.success)
            _logger.LogInformation("Invoices exported successfully");
        else
            _logger.LogWarning("Error exporting invoices: {result.Message}");
        return (result.success, result.message);
    }
}