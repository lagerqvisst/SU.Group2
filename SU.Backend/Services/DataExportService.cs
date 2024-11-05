using SU.Backend.Models.Comissions;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using SU.Backend.Services.Interfaces;
using SU.Backend.Models.Invoices;
using SU.Backend.Models.Insurances.Prospects;
using SU.Backend.Models.Statistics;
using System.Globalization;

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
                    worksheet.Cells[1, 6].Value = "Premium";

                    // Fyll på med data
                    for (int i = 0; i < invoices.Count; i++)
                    {
                        InvoiceEntry invoice = invoices[i];

                        string type = invoice.Type ?? "Unknown";
                        string customerOrCompanyName = type == "Privat" ? invoice.CustomerName : invoice.CompanyName;
                        string personalOrOrgNumber = type == "Privat" ? invoice.PersonalNumber : invoice.OrganizationNumber;
                        string contactPerson = type == "Företag" ? invoice.ContactPerson : "";
                        string address = invoice.Address ?? "";
                        decimal premium = invoice.Premium;

                        worksheet.Cells[i + 2, 1].Value = type;
                        worksheet.Cells[i + 2, 2].Value = customerOrCompanyName;
                        worksheet.Cells[i + 2, 3].Value = personalOrOrgNumber;
                        worksheet.Cells[i + 2, 4].Value = contactPerson;
                        worksheet.Cells[i + 2, 5].Value = address;
                        worksheet.Cells[i + 2, 6].Value = premium;
                    }

                    // Formatera kolumner för snyggare utseende
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Skapa ett unikt filnamn med datum och tid
                    var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"InvoiceReport_{timestamp}.xlsx");

                    // Spara filen
                    _logger.LogInformation($"Saving Excel file to {filePath}");
                    var file = new FileInfo(filePath);
                    await package.SaveAsAsync(file);

                    _logger.LogInformation("Excel file created successfully");

                    return (true, $"Excel file '{file.Name}' has been created successfully!\n\nYou can find the file here:\n{filePath}");
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

                    // Skapa rubriker
                    worksheet.Cells[1, 1].Value = "First / Company Name";
                    worksheet.Cells[1, 2].Value = "Last Name";
                    worksheet.Cells[1, 3].Value = "Personal / Org Nr";
                    worksheet.Cells[1, 4].Value = "Street Address";
                    worksheet.Cells[1, 5].Value = "Phone Number";
                    worksheet.Cells[1, 6].Value = "Email";
                    worksheet.Cells[1, 7].Value = "Agent Number";
                    worksheet.Cells[1, 8].Value = "Export Date";

                    // Tillämpa format på rubrikraden
                    using (var headerRange = worksheet.Cells[1, 1, 1, 8])
                    {
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        headerRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    }

                    var exportDate = DateTime.Now.ToShortDateString();
                    int currentRow = 2;

                    foreach (var prospect in prospects)
                    {
                        // Skriv ut grundläggande prospektdata
                        worksheet.Cells[currentRow, 1].Value = prospect.FirstName;
                        worksheet.Cells[currentRow, 2].Value = prospect.LastName;
                        worksheet.Cells[currentRow, 3].Value = prospect.PersonalOrOrgNumber;
                        worksheet.Cells[currentRow, 4].Value = prospect.StreetAddress;
                        worksheet.Cells[currentRow, 5].Value = prospect.PhoneNumber;
                        worksheet.Cells[currentRow, 6].Value = prospect.Email;
                        worksheet.Cells[currentRow, 7].Value = prospect.AgentNumber;
                        worksheet.Cells[currentRow, 8].Value = exportDate;

                        // Tillämpa ram på prospektsdata
                        for (int col = 1; col <= 8; col++)
                        {
                            worksheet.Cells[currentRow, col].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                            worksheet.Cells[currentRow, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                            worksheet.Cells[currentRow, col].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        }

                        currentRow++;

                        // Lägg till detaljerad information som anteckningar och status
                        worksheet.Cells[currentRow, 1].Value = "Contact Date:";
                        worksheet.Cells[currentRow, 2].Value = "";
                        worksheet.Cells[currentRow, 1, currentRow, 2].Style.Font.Bold = true;

                        worksheet.Cells[currentRow + 1, 1].Value = "Outcome:";
                        worksheet.Cells[currentRow + 1, 2].Value = "";
                        worksheet.Cells[currentRow + 1, 1, currentRow + 1, 2].Style.Font.Bold = true;

                        worksheet.Cells[currentRow + 2, 1].Value = "Seller:";
                        worksheet.Cells[currentRow + 2, 2].Value = "";
                        worksheet.Cells[currentRow + 2, 1, currentRow + 2, 2].Style.Font.Bold = true;

                        worksheet.Cells[currentRow + 3, 1].Value = "Agent Number:";
                        worksheet.Cells[currentRow + 3, 2].Value = "";
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


        public async Task<(bool success, string message)> ExportSellerStatisticsToExcel(List<SellerStatistics> sellerStatistics, bool isPrivateInsurance)
{
    try
    {
        if (sellerStatistics == null || !sellerStatistics.Any())
        {
            _logger.LogInformation("No seller statistics to export");
            return (false, "No seller statistics to export");
        }

        _logger.LogInformation("Exporting seller statistics to Excel");
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Seller Statistics");

            // Header for Year (no merging)
            worksheet.Cells[1, 1].Value = DateTime.Now.Year.ToString();

            // Column Headers
            worksheet.Cells[2, 1].Value = "Seller/Insurance";
            worksheet.Cells[2, 2].Value = "Agent Number";

            var categories = isPrivateInsurance ? new[] { "Child", "Adult", "Life" } : new[] { "Property", "Vehicle", "Liability" };

            // Setting up headers for each month without merging
            int startColumn = 3;
            for (int month = 0; month < 12; month++)
            {
                var monthName = new CultureInfo("en-US").DateTimeFormat.GetMonthName(month + 1);
                worksheet.Cells[1, startColumn].Value = monthName;

                // Set category headers under each month
                for (int i = 0; i < categories.Length; i++)
                {
                    worksheet.Cells[2, startColumn + i].Value = categories[i];
                }
                worksheet.Cells[2, startColumn + categories.Length].Value = "Total"; // Monthly Total
                startColumn += categories.Length + 1; // Move to the next month's start column
            }

            // Yearly Summary Columns
            worksheet.Cells[1, startColumn].Value = "Whole Year";
            worksheet.Cells[2, startColumn].Value = "Total";
            worksheet.Cells[2, startColumn + 1].Value = "Avg/Month";

            // Fill data rows
            int row = 3;
            foreach (var sellerStat in sellerStatistics)
            {
                worksheet.Cells[row, 1].Value = sellerStat.SellerName;
                worksheet.Cells[row, 2].Value = sellerStat.AgentNumber;

                startColumn = 3;
                foreach (var monthlyData in sellerStat.MonthlySales)
                {
                    if (isPrivateInsurance)
                    {
                        worksheet.Cells[row, startColumn].Value = monthlyData.ChildInsuranceSales;
                        worksheet.Cells[row, startColumn + 1].Value = monthlyData.AdultInsuranceSales;
                        worksheet.Cells[row, startColumn + 2].Value = monthlyData.LifeInsuranceSales;
                        worksheet.Cells[row, startColumn + 3].Value = monthlyData.TotalSales;
                    }
                    else
                    {
                        worksheet.Cells[row, startColumn].Value = monthlyData.PropertyInsuranceSales;
                        worksheet.Cells[row, startColumn + 1].Value = monthlyData.VehicleInsuranceSales;
                        worksheet.Cells[row, startColumn + 2].Value = monthlyData.LiabilityInsuranceSales;
                        worksheet.Cells[row, startColumn + 3].Value = monthlyData.TotalSales;
                    }
                    startColumn += categories.Length + 1;
                }

                // Yearly totals
                worksheet.Cells[row, startColumn].Value = sellerStat.TotalYearlySales;
                worksheet.Cells[row, startColumn + 1].Value = sellerStat.AverageMonthlySales;

                row++;
            }

            // Formatting
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            worksheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            worksheet.Cells[1, 1, 2, startColumn + 1].Style.Font.Bold = true;
            worksheet.Cells[1, 1, 2, startColumn + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            worksheet.Cells[1, 1, 2, startColumn + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

            // Apply thick bottom border to row 2
            var headerRowRange = worksheet.Cells[2, 1, 2, startColumn + 1];
            headerRowRange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;

            // Saving the file
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var fileType = isPrivateInsurance ? "PrivateSellerStats" : "CompanySellerStats";
            var fileName = $"{fileType}_{timestamp}.xlsx";
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            var file = new FileInfo(filePath);
            await package.SaveAsAsync(file);

            _logger.LogInformation("Excel file created successfully");

            return (true, $"Excel file '{fileName}' has been created successfully!\n\nYou can find the file here:\n{filePath}");
        }
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error exporting seller statistics to Excel");
        return (false, "Error exporting seller statistics to Excel");
    }
}





    }
}