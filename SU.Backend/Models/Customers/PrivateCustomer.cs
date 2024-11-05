using SU.Backend.Models.Insurances;

namespace SU.Backend.Models.Customers;

public class PrivateCustomer
{
    public int PrivateCustomerId { get; set; }
    public string PersonalNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }

    // Navigation property
    // A private customer can have many insurances, and therefor "be many insurance policy holders".
    public ICollection<InsurancePolicyHolder> InsurancePolicyHolders { get; set; }
}