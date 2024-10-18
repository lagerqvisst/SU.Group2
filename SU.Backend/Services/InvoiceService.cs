using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Helper;
using SU.Backend.Models.Invoices;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    /// <summary>
    /// This class is responsible for handling the business logic for invoice generation.
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
        /// Method to generate invoice data for all active insurances that should be invoiced this month.
        /// Uses helper functions to determine which insurances to invoice and to create the invoice data.
        /// </summary>
        /// <returns></returns>
        public async Task<(bool Success, string Message, List<InvoiceEntry> InvoiceData)> GenerateInvoiceData()
        {
            _logger.LogInformation("Starting the invoice data generation process...");

            try
            {
                DateTime currentDate = DateTime.Now; // Alltid aktuell månad

                // Get all active insurances
                var activeInsurances = await _unitOfWork.Insurances.GetAllActiveInsurances();

                // Filter insurances to be invoiced this month
                var insurancesToInvoice = activeInsurances
                    .Where(i => InvoiceHelper.ShouldInvoiceInsuranceThisMonth(i, currentDate)) // Använd den statiska hjälpfunktionen
                    .ToList();

                if (!insurancesToInvoice.Any())
                {
                    _logger.LogInformation("No insurances to invoice for this month.");
                    return (false, "No insurances to invoice for this month.", new List<InvoiceEntry>());
                }

                // Generate invoice data
                var invoiceData = insurancesToInvoice
                    .Select(i => InvoiceHelper.CreateInvoiceEntry(i)) // Använd den uppdaterade hjälpfunktionen
                    .ToList();

                return (true, "Invoice data generated successfully.", invoiceData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during invoice data generation");
                return (false, "An error occurred during invoice data generation.", new List<InvoiceEntry>());
            }
        }

    }
}
