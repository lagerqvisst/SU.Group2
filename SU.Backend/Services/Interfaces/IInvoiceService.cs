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
    /// This interface is responsible for defining the methods that the InvoiceService class must implement.
    /// </summary>
    public interface IInvoiceService
    {
        Task<(bool Success, string Message, List<InvoiceEntry> InvoiceData)> GenerateInvoiceData();
    }
}
