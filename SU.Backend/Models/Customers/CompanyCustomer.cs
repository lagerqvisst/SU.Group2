using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Customers
{
    public class CompanyCustomer
    {

        public int CompanyCustomerId { get; set; } // (PK)

        public string OrganizationNumber { get; set; } //Organisationsnummer

        public string CompanyName { get; set; } //Företagsnamn
            
        public string ContactPerson { get; set; } //Kontaktperson

        public string ContactPersonPhonenumber { get; set; } //Kontaktperson telefonnummer

        public string CompanyAdress { get; set; } //Företagsadress

        public string CompanyPhoneNumber { get; set; } //Företagstelefonnummer

        public string CompanyLandlineNumber { get; set; } //Företagsväxelnummer

        public string CompanyEmailAdress { get; set; } //Företagets e-postadress

        public ICollection<InsurancePolicyHolder> InsurancePolicyHolders { get; set; }


    }
}

