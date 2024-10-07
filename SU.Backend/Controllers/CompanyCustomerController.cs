using Microsoft.Extensions.Logging;
using SU.Backend.Models.Customers;
using SU.Backend.Services;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Controllers
{
    public class CompanyCustomerController
    {
        private readonly ICompanyCustomerService _companyCustomerService;
        private readonly ILogger<CompanyCustomerController> _logger;

        public CompanyCustomerController(ICompanyCustomerService companyCustomerService, ILogger<CompanyCustomerController> logger)
        {
            _companyCustomerService = companyCustomerService;
            _logger = logger;
        }
    }
}