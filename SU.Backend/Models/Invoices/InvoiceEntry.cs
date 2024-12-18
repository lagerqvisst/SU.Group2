﻿namespace SU.Backend.Models.Invoices;

/// <summary>
///     This class represents an invoice entry. Reflects the business documentation (provisioner)
///     Since a list of this information is used for exporting we dont use abstract classes.
/// </summary>
public class InvoiceEntry
{
    public string Type { get; set; }
    public string CustomerName { get; set; } // For private customers
    public string CompanyName { get; set; } // For companies
    public string PersonalNumber { get; set; } // For private customers
    public string OrganizationNumber { get; set; } // For companies
    public string ContactPerson { get; set; } // For companies
    public string Address { get; set; }
    public string Premium { get; set; }
    public string Insurances { get; set; } // Single string of all insurances
}