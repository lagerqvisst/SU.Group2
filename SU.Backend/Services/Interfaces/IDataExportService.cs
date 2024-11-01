using SU.Backend.Models.Comissions;
using SU.Backend.Models.Insurances.Prospects;
using SU.Backend.Models.Invoices;
using SU.Backend.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    /// <summary>
    /// This interface is responsible for defining the methods that the DataExportService class must implement.
    /// </summary>
    public interface IDataExportService
    {
        Task<(bool success, string message)> ExportCommissionsToExcel(List<Commission> commissions);
        Task<(bool success, string message)> ExportInvoicesToExcel(List<InvoiceEntry> invoices);
        Task<(bool success, string message)> ExportSellerStatisticsToExcel(List<SellerStatistics> sellerStatistics, bool isPrivateInsurance);
        Task<(bool success, string message)> ExportProspects(List<Prospect> prospects);

    }
}
