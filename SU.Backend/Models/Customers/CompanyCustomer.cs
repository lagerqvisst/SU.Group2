using SU.Backend.Models.Insurances;

namespace SU.Backend.Models.Customers;

public class CompanyCustomer
{
    public int CompanyCustomerId { get; set; } // (PK)

    public string OrganizationNumber { get; set; }

    public string CompanyName { get; set; }

    public string ContactPerson { get; set; }

    public string ContactPersonPhonenumber { get; set; }

    public string CompanyAdress { get; set; }

    public string CompanyPhoneNumber { get; set; }

    public string CompanyEmailAdress { get; set; }

    // Navigation property
    // A company customer can have many insurances, and therefor "be many insurance policy holders".
    public ICollection<InsurancePolicyHolder> InsurancePolicyHolders { get; set; }
}