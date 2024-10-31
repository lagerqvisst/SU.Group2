using SU.Backend.Models.Comissions;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using SU.Backend.Services.Interfaces;
using SU.Backend.Models.Invoices;
using SU.Backend.Models.Insurances.Prospects;

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
        public async Task<(bool success, string message)> ExportCommissionsToExcel(List<Commission> commissions)
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
                        worksheet.Cells[i + 2, 1].Value = commission.AgentNumber;
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
                    var fileName = $"CommissionReport_{timestamp}.xlsx";
                    var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                    // Spara filen
                    _logger.LogInformation($"Saving Excel file to {filePath}");
                    var file = new FileInfo(filePath);
                    await package.SaveAsAsync(file);

                    _logger.LogInformation("Excel file created successfully");

                    return (true, $"Excel file '{fileName}' has been created successfully!\n\nYou can find the file here:\n{filePath}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting commissions to Excel");
                return (false, "Error exporting commissions to Excel");
            }
        }

        // Method to export a list of invoices to an Excel file.
        public async Task<(bool success, string message)> ExportInvoicesToExcel(List<InvoiceEntry> invoices)
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

        public async Task<(bool success, string message)> ExportProspects(List<Prospect> prospects)
        {
            try
            {
                if (prospects == null || !prospects.Any())
                {
                    _logger.LogInformation("No prospects to export");
                    return (false, "No prospects to export");
                }

                _logger.LogInformation("Exporting prospects to Excel");
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Prospect Report");

                    // Skapa rubriker med fetstil och justering
                    worksheet.Cells[1, 1].Value = "First / Company Name";
                    worksheet.Cells[1, 2].Value = "Last Name";
                    worksheet.Cells[1, 3].Value = "Personal / Org Nr";
                    worksheet.Cells[1, 4].Value = "Street Address";
                    worksheet.Cells[1, 5].Value = "Phone Number";
                    worksheet.Cells[1, 6].Value = "Email";
                    worksheet.Cells[1, 7].Value = "Agent Number";
                    worksheet.Cells[1, 8].Value = "Export Date";

                    // Tillämpa fetstil på rubrikraden
                    using (var headerRange = worksheet.Cells[1, 1, 1, 8])
                    {
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        headerRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    }

                    var exportDate = DateTime.Now.ToShortDateString();
                    int currentRow = 2; // Starta från rad 2 eftersom första raden är rubriker

                    // Iterera över prospects och skriv ut data samt sammanfattning direkt under
                    foreach (var prospect in prospects)
                    {
                        // Prospect data med ram och justering
                        worksheet.Cells[currentRow, 1].Value = prospect.FirstName;
                        worksheet.Cells[currentRow, 2].Value = prospect.LastName;
                        worksheet.Cells[currentRow, 3].Value = prospect.PersonalOrOrgNumber;
                        worksheet.Cells[currentRow, 4].Value = prospect.StreetAddress;
                        worksheet.Cells[currentRow, 5].Value = prospect.PhoneNumber;
                        worksheet.Cells[currentRow, 6].Value = prospect.Email;
                        worksheet.Cells[currentRow, 7].Value = prospect.AssignedAgentNumber;
                        worksheet.Cells[currentRow, 8].Value = exportDate;

                        // Tillämpa ram på hela prospect-raden
                        for (int col = 1; col <= 8; col++)
                        {
                            worksheet.Cells[currentRow, col].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                            worksheet.Cells[currentRow, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                            worksheet.Cells[currentRow, col].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        }

                        currentRow++;

                        // Sammanfattning med fetstil och ram
                        worksheet.Cells[currentRow, 1].Value = "Contact Date:";
                        worksheet.Cells[currentRow, 2].Value = prospect.ContactDate?.ToShortDateString() ?? "";
                        worksheet.Cells[currentRow, 1, currentRow, 2].Style.Font.Bold = true;

                        worksheet.Cells[currentRow + 1, 1].Value = "Outcome:";
                        worksheet.Cells[currentRow + 1, 2].Value = prospect.ContactNote ?? "";
                        worksheet.Cells[currentRow + 1, 1, currentRow + 1, 2].Style.Font.Bold = true;

                        worksheet.Cells[currentRow + 2, 1].Value = "Seller:";
                        worksheet.Cells[currentRow + 2, 2].Value = $"{prospect.Seller?.FirstName} {prospect.Seller?.LastName}";
                        worksheet.Cells[currentRow + 2, 1, currentRow + 2, 2].Style.Font.Bold = true;

                        worksheet.Cells[currentRow + 3, 1].Value = "Agency Number:";
                        worksheet.Cells[currentRow + 3, 2].Value = prospect.AssignedAgentNumber;
                        worksheet.Cells[currentRow + 3, 1, currentRow + 3, 2].Style.Font.Bold = true;

                        // Tillämpa ramar på varje cell i sammanfattningen
                        for (int row = currentRow; row <= currentRow + 3; row++)
                        {
                            worksheet.Cells[row, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                            worksheet.Cells[row, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        }

                        // Öka currentRow för nästa prospect, lämnar en tom rad för bättre läsbarhet
                        currentRow += 5;
                    }

                    // AutoFit kolumner för att passa innehållet
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Spara filen
                    var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"ProspectReport_{timestamp}.xlsx");
                    _logger.LogInformation($"Saving Excel file to {filePath}");
                    var file = new FileInfo(filePath);
                    await package.SaveAsAsync(file);

                    _logger.LogInformation("Excel file created successfully");

                    return (true, $"Excel file '{file.Name}' has been created successfully!\n\nYou can find the file here:\n{filePath}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting prospects to Excel");
                return (false, "Error exporting prospects to Excel");
            }
        }





    }
}
