using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public class InsurancePolicyHolderRepository : Repository<InsurancePolicyHolder>, IInsurancePolicyHolderRepository
    {
        public InsurancePolicyHolderRepository(Context context) : base(context)
        {
        }

        public async Task<InsurancePolicyHolder> GetById(InsurancePolicyHolder insurancePolicyHolder)
        {
            return await _context.InsurancePolicyHolders.FindAsync(insurancePolicyHolder);
        }

        public  Task<List<InsurancePolicyHolder>> IdentifyProspects()
        {
            return null;
        }
    }
}
