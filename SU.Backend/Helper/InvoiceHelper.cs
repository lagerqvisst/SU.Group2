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

        // This method determines if an insurance should be invoiced this month.
        // Its based on the logic from the business documentation.
        public static bool ShouldInvoiceInsuranceThisMonth(Insurance insurance, DateTime currentDate)
        {
            int monthsSinceStart = ((currentDate.Year - insurance.StartDate.Year) * 12) + currentDate.Month - insurance.StartDate.Month;

            switch (insurance.PaymentPlan)
            {
                case PaymentPlan.Monthly:
                    return true; // Invoice every month
                case PaymentPlan.Quarterly:
                    return monthsSinceStart % 3 == 0; // Invoice every third month
                case PaymentPlan.SemiAnnual:
                    return monthsSinceStart % 6 == 0; // Invoice every sixth month
                case PaymentPlan.Annual:
                    return monthsSinceStart % 12 == 0; // Invoice every twelfth month
                default:
                    return false;
            }
        }

        // This method creates an invoice entry based on the insurance data.
        // Its called after logic such as the method above has been applied.
        // Used for exporting invoice data to external systems.
        public static InvoiceEntry CreateInvoiceEntry(Insurance insurance)
        {
            if (insurance.InsurancePolicyHolder.PrivateCustomer != null)
            {
                return new InvoiceEntry
                {
                    Type = "Privat",
                    CustomerName = $"{insurance.InsurancePolicyHolder.PrivateCustomer.FirstName} {insurance.InsurancePolicyHolder.PrivateCustomer.LastName}",
                    PersonalNumber = insurance.InsurancePolicyHolder.PrivateCustomer.PersonalNumber,
                    Address = insurance.InsurancePolicyHolder.PrivateCustomer.Address,
                    PostalCode = insurance.InsurancePolicyHolder.PrivateCustomer.Address,
                    Premium = insurance.Premium
                };
            }
            else if (insurance.InsurancePolicyHolder.CompanyCustomer != null)
            {
                return new InvoiceEntry
                {
                    Type = "Företag",
                    CompanyName = insurance.InsurancePolicyHolder.CompanyCustomer.CompanyName,
                    OrganizationNumber = insurance.InsurancePolicyHolder.CompanyCustomer.OrganizationNumber,
                    ContactPerson = insurance.InsurancePolicyHolder.CompanyCustomer.ContactPerson,
                    Address = insurance.InsurancePolicyHolder.CompanyCustomer.CompanyAdress,
                    PostalCode = insurance.InsurancePolicyHolder.CompanyCustomer.CompanyAdress,
                    Premium = insurance.Premium
                };
            }
            return null;
        }



    }
}
