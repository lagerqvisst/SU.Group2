﻿using SU.Backend.Models.Customers;
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

        Task<(bool Success, string Message, CompanyCustomer Customer)> CreateCompanyCustomer(CompanyCustomer newCompanyCustomer);

        Task<(bool Success, string Message, CompanyCustomer Customer)> UpdateCompanyCustomer(CompanyCustomer companyCustomer);

        Task<(bool Success, string Message, CompanyCustomer Customer)> DeleteCompanyCustomer(CompanyCustomer companyCustomer);
    }

}

