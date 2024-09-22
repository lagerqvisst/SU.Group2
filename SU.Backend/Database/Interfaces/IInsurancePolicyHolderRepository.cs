using SU.Backend.Models.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Interfaces
{
    public interface IInsurancePolicyHolderRepository
    {
        Task<InsurancePolicyHolder> GetById(InsurancePolicyHolder insurancePolicyHolder);
    }
}
