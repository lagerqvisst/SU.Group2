using SU.Backend.Models.Comissions;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using SU.Backend.Services.Interfaces;

namespace SU.Backend.Services
{
    public class DataExportService : IDataExportService
    {
        private readonly ILogger<DataExportService> _logger;

        public DataExportService(ILogger<DataExportService> logger)
        {
            _logger = logger;
        }

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

    }
}
