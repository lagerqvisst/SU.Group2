using SU.Backend.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    public interface ICompanyCustomerService
    {
        Task<(bool Success, string Message, CompanyCustomer Customer)> GenerateTestCompanyCustomer();

    }
}
