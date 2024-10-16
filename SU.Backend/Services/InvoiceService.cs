using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Helper;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ILogger<InvoiceService> _logger;
        private readonly UnitOfWork _unitOfWork;

        public InvoiceService(ILogger<InvoiceService> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool Success, string Message, List<object> InvoiceData)> GenerateInvoiceData()
        {
            _logger.LogInformation("Starting the invoice data generation process...");

            try
            {
                DateTime currentDate = DateTime.Now; // Alltid aktuell månad

                // Hämta alla aktiva försäkringar
                var activeInsurances = await _unitOfWork.Insurances.GetAllActiveInsurances();

                // Filtrera försäkringar som ska faktureras denna månad
                var insurancesToInvoice = activeInsurances
                    .Where(i => InvoiceHelper.ShouldInvoiceInsuranceThisMonth(i, currentDate)) // Använd den statiska hjälpfunktionen
                    .ToList();

                if (!insurancesToInvoice.Any())
                {
                    _logger.LogInformation("No insurances to invoice for this month.");
                    return (false, "No insurances to invoice for this month.", new List<object>());
                }

                // Generera fakturadata
                var invoiceData = insurancesToInvoice
                    .Select(i => InvoiceHelper.CreateInvoiceEntry(i)) // Använd den statiska hjälpfunktionen
                    .ToList();

                return (true, "Invoice data generated successfully.", invoiceData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during invoice data generation");
                return (false, "An error occurred during invoice data generation.", new List<object>());
            }
        }
    }
}
