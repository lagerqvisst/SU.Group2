namespace SU.Backend.Models.Comissions;

/// <summary>
///     This class is responsible for defining the properties of a commission.
/// </summary>
public class Commission
{
    public string AgentNumber { get; set; }
    public string SellerName { get; set; }
    public string CommissionAmount { get; set; }
    public string PersonalNumber { get; set; }
    public DateTime StartDate { get; set; } // Defines the search range for commissions
    public DateTime EndDate { get; set; }


    // Method to calculate commission and return it as a formatted string with "SEK"
    public static string CalculateCommission(decimal premium)
    {
        var commission = premium * 0.12m;
        var roundedCommission = Math.Round(commission, MidpointRounding.AwayFromZero); // Round to nearest whole number
        return $"{roundedCommission:N0} SEK"; // Format with no decimal places and append "SEK"
    }
}