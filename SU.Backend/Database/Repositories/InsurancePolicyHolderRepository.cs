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
    /// <summary>
    /// This class is responsible for implementing the methods defined in the IInsurancePolicyHolderRepository interface.
    /// </summary>
    public class InsurancePolicyHolderRepository : Repository<InsurancePolicyHolder>, IInsurancePolicyHolderRepository
    {
        public InsurancePolicyHolderRepository(Context context) : base(context)
        {
        }

        public async Task<InsurancePolicyHolder> GetById(InsurancePolicyHolder insurancePolicyHolder)
        {
            return await _context.InsurancePolicyHolders.FindAsync(insurancePolicyHolder);
        }


        public async Task<List<InsurancePolicyHolder>> GetAllInsurancePolicyHolders()
        {
            return await _context.InsurancePolicyHolders.ToListAsync();
        }
    }
}
