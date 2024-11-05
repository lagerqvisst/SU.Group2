using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Models.Insurances.Prospects;
using SU.Backend.Services.Interfaces;

namespace SU.Backend.Services;

/// <summary>
///     This class is responsible for handling the logic for identifying prospects and assigning sellers to them
/// </summary>
public class ProspectService : IProspectService
{
    private readonly ILogger<ProspectService> _logger;
    private readonly UnitOfWork _unitOfWork;

    public ProspectService(UnitOfWork unitOfWork, ILogger<ProspectService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    ///     This method identifies prospects based on specific criteria and adds them to the database
    ///     Identification is based on the number of insurances the customer has which aligns with the business documentation.
    ///     A prospect is considered a customer with 1 insurance only.
    /// </summary>
    /// <returns></returns>
    public async Task<(bool success, string message, List<Prospect> prospects)> GenerateProspectData()
    {
        _logger.LogInformation("Generating prospects without saving to the database");

        try
        {
            var prospects = new List<Prospect>();

            // Hämta privatkunder och företagskunder med endast en försäkring
            var privateCustomers = await _unitOfWork.PrivateCustomers.GetProspectDataForPrivateCustomers();
            var companyCustomers = await _unitOfWork.CompanyCustomers.GetProspectDataForCompanyCustomers();

            // Lägg till privatkunder som prospekt
            if (privateCustomers.Any())
                prospects.AddRange(privateCustomers.Select(pc => new Prospect
                {
                    FirstName = pc.FirstName,
                    LastName = pc.LastName,
                    PersonalOrOrgNumber = pc.PersonalNumber,
                    StreetAddress = pc.Address,
                    PhoneNumber = pc.PhoneNumber,
                    Email = pc.Email,
                    AgentNumber = pc.InsurancePolicyHolders.First().Insurance.Seller.AgentNumber,
                    Seller =
                        $"{pc.InsurancePolicyHolders.First().Insurance.Seller.FirstName} {pc.InsurancePolicyHolders.First().Insurance.Seller.LastName}"
                }));

            // Lägg till företagskunder som prospekt
            if (companyCustomers.Any())
                prospects.AddRange(companyCustomers.Select(cc => new Prospect
                {
                    FirstName = cc.CompanyName,
                    LastName = string.Empty,
                    PersonalOrOrgNumber = cc.OrganizationNumber,
                    StreetAddress = cc.CompanyAdress,
                    PhoneNumber = cc.CompanyPhoneNumber,
                    Email = cc.CompanyEmailAdress,
                    AgentNumber = cc.InsurancePolicyHolders.First().Insurance.Seller.AgentNumber,
                    Seller =
                        $"{cc.InsurancePolicyHolders.First().Insurance.Seller.FirstName} {cc.InsurancePolicyHolders.First().Insurance.Seller.LastName}"
                }));

            if (prospects.Any())
            {
                _logger.LogInformation($"Generated {prospects.Count} prospects");
                return (true, $"{prospects.Count} prospects generated successfully", prospects);
            }

            _logger.LogInformation("No prospects found");
            return (false, "No prospects found", prospects);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating prospects");
            return (false, "Error generating prospects", new List<Prospect>());
        }
    }
}