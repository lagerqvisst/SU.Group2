using SU.Backend.Models.Employees;
using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Frontend.Helper.DI_Objects.InsuranceObjects
{
    public interface IPolicyHolderService
    {
        InsurancePolicyHolder InsurancePolicyHolder { get; set; }

    }
}
