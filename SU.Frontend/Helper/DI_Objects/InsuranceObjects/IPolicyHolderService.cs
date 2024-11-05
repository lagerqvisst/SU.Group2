using SU.Backend.Models.Insurances;

namespace SU.Frontend.Helper.DI_Objects.InsuranceObjects;

public interface IPolicyHolderService
{
    InsurancePolicyHolder InsurancePolicyHolder { get; set; }
}