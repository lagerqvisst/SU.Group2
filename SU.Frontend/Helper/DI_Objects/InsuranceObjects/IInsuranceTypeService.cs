using SU.Backend.Models.Enums.Insurance;

namespace SU.Frontend.Helper.DI_Objects.InsuranceObjects;

public interface IInsuranceTypeService
{
    InsuranceType InsuranceType { get; set; }
}