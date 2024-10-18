using SU.Backend.Models.Comissions;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using SU.Backend.Services.Interfaces;
using SU.Backend.Models.Invoices;

namespace SU.Backend.Services
{
    /// <summary>
    /// This class is responsible for handling the business logic for exporting data to Excel.
    /// It is using EPPPlus library to create Excel files.
    /// By using this we can make any of our entitie lists to Excel files.
    /// </summary>
    public class DataExportService : IDataExportService
    {
        private readonly ILogger<DataExportService> _logger;

        public DataExportService(ILogger<DataExportService> logger)
        {
            _logger = logger;
        }

        // Method to export a list of commissions to an Excel file.
        public async Task<(bool Success, string Message)> ExportCommissionsToExcel(List<Commission> commissions)
        {
            try
            {
                if (commissions == null || !commissions.Any())
                {
                    _logger.LogInformation("No commissions to export");
                    return (false, "No commissions to export");
                }

                _logger.LogInformation("Exporting commissions to Excel");
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Commission Report");

                    // Rubriker
                    worksheet.Cells[1, 1].Value = "Agent Number";
                    worksheet.Cells[1, 2].Value = "Seller Name";
                    worksheet.Cells[1, 3].Value = "Personal Number";
                    worksheet.Cells[1, 4].Value = "Start Date";
                    worksheet.Cells[1, 5].Value = "End Date";
                    worksheet.Cells[1, 6].Value = "Commission Amount";

                    // Fyll på med data
                    for (int i = 0; i < commissions.Count; i++)
                    {
                        var commission = commissions[i];
                        worksheet.Cells[i + 2, 1].Value = commission.Seller.AgentNumber;
                        worksheet.Cells[i + 2, 2].Value = commission.SellerName;
                        worksheet.Cells[i + 2, 3].Value = commission.PersonalNumber;
                        worksheet.Cells[i + 2, 4].Value = commission.StartDate.ToShortDateString();
                        worksheet.Cells[i + 2, 5].Value = commission.EndDate.ToShortDateString();
                        worksheet.Cells[i + 2, 6].Value = commission.CommissionAmount;
                    }

                    // Formatera kolumner för snyggare utseende
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Skapa ett unikt filnamn med datum och tid
                    var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    var filePath = $"CommissionReport_{timestamp}.xlsx";

                    // Spara filen
                    _logger.LogInformation($"Saving Excel file to {filePath}");
                    var file = new FileInfo(filePath);
                    await package.SaveAsAsync(file);

                    _logger.LogInformation("Excel file created successfully");

                    return (true, "Excel file created successfully");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting commissions to Excel");
                return (false, "Error exporting commissions to Excel");
            }
        }

        // Method to export a list of invoices to an Excel file.
        public async Task<(bool Success, string Message)> ExportInvoicesToExcel(List<InvoiceEntry> invoices)
        {
            try
            {
                _logger.LogInformation("Exporting invoices to Excel");
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Invoice Report");

                    // Rubriker
                    worksheet.Cells[1, 1].Value = "Type";
                    worksheet.Cells[1, 2].Value = "Customer Name / Company Name";
                    worksheet.Cells[1, 3].Value = "Personal Number / Org Number";
                    worksheet.Cells[1, 4].Value = "Contact Person";  // Endast för företag
                    worksheet.Cells[1, 5].Value = "Address";
                    worksheet.Cells[1, 6].Value = "Postal Code";
                    worksheet.Cells[1, 7].Value = "Premium";

                    // Fyll på med data
                    for (int i = 0; i < invoices.Count; i++)
                    {
                        InvoiceEntry invoice = invoices[i];

                        string type = invoice.Type ?? "Unknown";
                        string customerOrCompanyName = type == "Privat" ? invoice.CustomerName : invoice.CompanyName;
                        string personalOrOrgNumber = type == "Privat" ? invoice.PersonalNumber : invoice.OrganizationNumber;
                        string contactPerson = type == "Företag" ? invoice.ContactPerson : "";
                        string address = invoice.Address ?? "";
                        string postalCode = invoice.PostalCode ?? "";
                        decimal premium = invoice.Premium;

                        worksheet.Cells[i + 2, 1].Value = type;
                        worksheet.Cells[i + 2, 2].Value = customerOrCompanyName;
                        worksheet.Cells[i + 2, 3].Value = personalOrOrgNumber;
                        worksheet.Cells[i + 2, 4].Value = contactPerson;
                        worksheet.Cells[i + 2, 5].Value = address;
                        worksheet.Cells[i + 2, 6].Value = postalCode;
                        worksheet.Cells[i + 2, 7].Value = premium;
                    }


                    // Formatera kolumner för snyggare utseende
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Skapa ett unikt filnamn med datum och tid
                    var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    var filePath = $"InvoiceReport_{timestamp}.xlsx";


                    // Spara filen
                    _logger.LogInformation($"Saving Excel file to {filePath}");
                    var file = new FileInfo(filePath);
                    await package.SaveAsAsync(file);

                    _logger.LogInformation("Excel file created successfully");

                    return (true, "Excel file created successfully");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting invoices to Excel");
                return (false, "Error exporting invoices to Excel");
            }
        }


    }
}
