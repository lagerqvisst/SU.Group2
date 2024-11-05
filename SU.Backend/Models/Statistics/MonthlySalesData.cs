using SU.Backend.Models.Enums.Insurance;

namespace SU.Backend.Models.Statistics;

/// <summary>
///     Create to store the monthly sales data for each insurance type.
/// </summary>
public class MonthlySalesData
{
    public int Month { get; set; } // 1 = January, 2 = February, ..., 12 = December

    // Sales counts per insurance type
    public Dictionary<InsuranceType, int> InsuranceSalesCounts { get; set; } = new();

    public int TotalSales => InsuranceSalesCounts.Values.Sum();

    // Properties for each insurance type
    public int ChildInsuranceSales => InsuranceSalesCounts.ContainsKey(InsuranceType.ChildAccidentAndHealthInsurance)
        ? InsuranceSalesCounts[InsuranceType.ChildAccidentAndHealthInsurance]
        : 0;

    public int AdultInsuranceSales => InsuranceSalesCounts.ContainsKey(InsuranceType.AdultAccidentAndHealthInsurance)
        ? InsuranceSalesCounts[InsuranceType.AdultAccidentAndHealthInsurance]
        : 0;

    public int LifeInsuranceSales => InsuranceSalesCounts.ContainsKey(InsuranceType.AdultLifeInsurance)
        ? InsuranceSalesCounts[InsuranceType.AdultLifeInsurance]
        : 0;

    public int PropertyInsuranceSales => InsuranceSalesCounts.ContainsKey(InsuranceType.PropertyAndInventoryInsurance)
        ? InsuranceSalesCounts[InsuranceType.PropertyAndInventoryInsurance]
        : 0;

    public int VehicleInsuranceSales => InsuranceSalesCounts.ContainsKey(InsuranceType.VehicleInsurance)
        ? InsuranceSalesCounts[InsuranceType.VehicleInsurance]
        : 0;

    public int LiabilityInsuranceSales => InsuranceSalesCounts.ContainsKey(InsuranceType.LiabilityInsurance)
        ? InsuranceSalesCounts[InsuranceType.LiabilityInsurance]
        : 0;

    // Total sales for private insurance types
    public int TotalSalesPrivate => ChildInsuranceSales + AdultInsuranceSales + LifeInsuranceSales;

    // Total sales for company insurance types
    public int TotalSalesCompany => PropertyInsuranceSales + VehicleInsuranceSales + LiabilityInsuranceSales;
}