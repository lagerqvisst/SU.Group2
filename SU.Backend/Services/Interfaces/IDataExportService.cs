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
        Task<(bool success, string message)> ExportCommissionsToExcel(List<Commission> commissions);
        Task<(bool success, string message)> ExportInvoicesToExcel(List<InvoiceEntry> invoices);
    }
}
