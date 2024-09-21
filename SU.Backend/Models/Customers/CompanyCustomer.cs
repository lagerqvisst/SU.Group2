using SU.Backend.Models.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Customers
{
    public class CompanyCustomer
    {
        public int CompanyCustomerId { get; set; } //Organisationsnummer (PK)

        public string CompanyName { get; set; } //Företagsnamn
            
        public string ContactPerson { get; set; } //Kontaktperson

        public string ContactPersonPhonenumber { get; set; } //Kontaktperson telefonnummer

        public string CompanyAdress { get; set; } //Företagsadress

        public string CompanyPhoneNumber { get; set; } //Företagstelefonnummer

        public string CompanyLandlineNumber { get; set; } //Företagsväxelnummer

        public string CompanyEmailAdress { get; set; } //Företagets e-postadress

    }
}

