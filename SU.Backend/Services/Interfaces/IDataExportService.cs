using SU.Backend.Models.Comissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    public interface IDataExportService
    {
        Task<(bool Success, string Message)> ExportCommissionsToExcel(List<Commission> commissions);
    }
}
