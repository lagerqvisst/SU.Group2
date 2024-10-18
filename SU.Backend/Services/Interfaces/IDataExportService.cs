using SU.Backend.Models.Comissions;
using SU.Backend.Models.Invoices;
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
        Task<(bool Success, string Message)> ExportCommissionsToExcel(List<Commission> commissions);
        Task<(bool Success, string Message)> ExportInvoicesToExcel(List<InvoiceEntry> invoices);
    }
}
