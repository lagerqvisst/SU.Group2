using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances;
using SU.Backend.Models.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Helper
{
    /// <summary>
    /// This class contains helper methods for the invoice.
    /// It was created to improve readability in the invoice service methods
    /// </summary>
    public static class InvoiceHelper
    {


        public static InvoiceEntry CreateInvoiceEntry(InsurancePolicyHolder policyHolder)
        {
            // Samla alla försäkrings-ID kopplade till denna policyinnehavare
            var allInsuranceIds = policyHolder.CompanyCustomer != null
                ? policyHolder.CompanyCustomer.InsurancePolicyHolders
                    .Select(p => p.Insurance.InsuranceId.ToString())
                : policyHolder.PrivateCustomer?.InsurancePolicyHolders
                    .Select(p => p.Insurance.InsuranceId.ToString()) ?? new List<string>();

            // Bygg strängen med "ID: " följt av alla ID:n
            string insurancesAsString = $"ID: {string.Join(", ", allInsuranceIds)}";

            // Formatera Premium till sträng med SEK
            string formattedPremium = $"{policyHolder.Insurance.Premium:N0} SEK";

            if (policyHolder.PrivateCustomer != null)
            {
                return new InvoiceEntry
                {
                    Type = "Privat",
                    CustomerName = $"{policyHolder.PrivateCustomer.FirstName} {policyHolder.PrivateCustomer.LastName}",
                    PersonalNumber = policyHolder.PrivateCustomer.PersonalNumber,
                    Address = policyHolder.PrivateCustomer.Address,
                    Premium = formattedPremium,
                    Insurances = insurancesAsString
                };
            }
            else if (policyHolder.CompanyCustomer != null)
            {
                return new InvoiceEntry
                {
                    Type = "Företag",
                    CompanyName = policyHolder.CompanyCustomer.CompanyName,
                    OrganizationNumber = policyHolder.CompanyCustomer.OrganizationNumber,
                    ContactPerson = policyHolder.CompanyCustomer.ContactPerson,
                    Address = policyHolder.CompanyCustomer.CompanyAdress,
                    Premium = formattedPremium,
                    Insurances = insurancesAsString
                };
            }
            return null;
        }


    }
}
