using SU.Backend.Models.Insurances;

namespace SU.Frontend.Helper.DI_Objects.InsuranceObjects;

public class PolicyHolderService : IPolicyHolderService
{
    public InsurancePolicyHolder InsurancePolicyHolder { get; set; }
}