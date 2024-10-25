using SU.Backend.Models.Customers;
using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Frontend.Helper.InsuranceObjects
{
    public interface IPolicyHolderService
    {
        InsurancePolicyHolder SelectedPolicyHolder { get; set; }
    }
}
