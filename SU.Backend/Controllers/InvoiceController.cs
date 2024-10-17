using Microsoft.Extensions.Logging;
using SU.Backend.Models.Invoices;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Controllers
{
    public class InvoiceController
    {
        IInvoiceService _invoiceService;
        ILogger<InvoiceController> _logger;

        public InvoiceController(IInvoiceService invoiceService, ILogger<InvoiceController> logger)
        {
            _invoiceService = invoiceService;
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
    }
}
