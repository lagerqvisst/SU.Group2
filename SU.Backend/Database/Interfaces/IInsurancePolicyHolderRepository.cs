using SU.Backend.Models.Insurances;

namespace SU.Backend.Database.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the InsurancePolicyHolderRepository class must
///     implement.
/// </summary>
public interface IInsurancePolicyHolderRepository
{
    Task<InsurancePolicyHolder> GetById(InsurancePolicyHolder insurancePolicyHolder);

    Task<List<InsurancePolicyHolder>> GetAllInsurancePolicyHolders();

    Task<List<InsurancePolicyHolder>> GetAllPolicyHoldersWithInsurances();
}