using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Enums
{
    /// <summary>
    /// Represents all the roles from the org-chart in the business documentation.
    /// </summary>
    public enum EmployeeType
    {
        OutsideSales,
        InsideSales,
        SalesAssistant,
        FinancialAssistant,
        SalesManager,
        CEO
    }
}
