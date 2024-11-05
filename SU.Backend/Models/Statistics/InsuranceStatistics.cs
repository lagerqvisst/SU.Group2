namespace SU.Backend.Models.Statistics;

/// <summary>
///     This class represents the statistics for the insurance. Reflects the business documentation.
/// </summary>
public class InsuranceStatistics
{
    public string InsuranceType { get; set; }
    public int TotalPolicies { get; set; }
    public decimal TotalPremium { get; set; }
    public int Month { get; set; }
}