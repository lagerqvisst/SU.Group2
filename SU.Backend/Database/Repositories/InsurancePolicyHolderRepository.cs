using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public class InsurancePolicyHolderRepository : Repository<InsurancePolicyHolder>
    {
        public InsurancePolicyHolderRepository(Context context) : base(context)
        {
        }
    }
}
