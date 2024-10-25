using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Frontend.Helper.DI_Objects.InsuranceObjects
{
    public class PolicyHolderService : IPolicyHolderService
    {
        public InsurancePolicyHolder InsurancePolicyHolder { get; set; }
    }
}
