using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public class CompanyCustomerRepository : Repository<CompanyCustomer>, ICompanyCustomerRepository
    {
        public CompanyCustomerRepository(Context context) : base(context)
        {
        }
    }
}
