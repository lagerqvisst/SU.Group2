﻿using Microsoft.Extensions.Logging;
using SU.Backend.Models.Invoices;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Controllers
{
    /// <summary>
    /// This class is responsible for handling the invoice controller.
    /// Makes logic available in the Viewmodel
    /// More info about the logic for each method can be found in the Service function each controller method uses.
    /// </summary>
    public class InvoiceController
    {
        IInvoiceService _invoiceService;
        IDataExportService _dataExportService;
        ILogger<InvoiceController> _logger;

        public InvoiceController(IInvoiceService invoiceService, IDataExportService dataExportService, ILogger<InvoiceController> logger)
        {
            _invoiceService = invoiceService;
            _dataExportService = dataExportService;
            _logger = logger;
        }

        public async Task<(bool Success, string Message, List<InvoiceEntry> InvoiceData)> GenerateInvoiceData()
        {
            _logger.LogInformation("Controller activated to generate invoice data...");

            var result = await _invoiceService.GenerateInvoiceData();

            if (result.Success)
            {
                _logger.LogInformation($"Invoice data generated successfully:\n{result.Message}");
                return (result.Success, result.Message, result.InvoiceData);
            }
            else
            {
                _logger.LogWarning($"Error generating invoice data: {result.Message}");
                return (result.Success, result.Message, new List<InvoiceEntry>());
            }
        }

        public async Task<(bool Success, string Message)> ExportInvoicesToExcel(List<InvoiceEntry> invoices)
        {
            _logger.LogInformation("Exporting invoices to Excel...");

            var result = await _dataExportService.ExportInvoicesToExcel(invoices);

            if (result.Success)
            {
                _logger.LogInformation("Invoices exported successfully");
            }
            else
            {
                _logger.LogWarning("Error exporting invoices: {result.Message}");
            }
            return (result.Success, result.Message);
        }
}
    }
