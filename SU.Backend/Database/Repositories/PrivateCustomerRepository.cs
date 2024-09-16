using SU.Backend.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public class PrivateCustomerRepository : Repository<PrivateCustomer>
    {
        public PrivateCustomerRepository(DbConnection context) : base(context)
        {
        }
    }

}
