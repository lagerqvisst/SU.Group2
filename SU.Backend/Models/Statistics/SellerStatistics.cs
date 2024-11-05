namespace SU.Backend.Models.Statistics;

public class SellerStatistics
{
    // Basic information about the seller
    public string SellerName { get; set; }
    public string AgentNumber { get; set; }

    // Monthly sales data
    public List<MonthlySalesData> MonthlySales { get; set; } = new();

    // Yearly totals and averages
    public int TotalYearlySales { get; set; } // Sum of all insurances sold throughout the year
    public double AverageMonthlySales { get; set; } // Average sales per month

    // Optional: Store the year for context
    public int Year { get; set; }

    public static List<double> CalculateMovingAverage(int[] values, int period)
    {
        var movingAverage = new List<double>();
        for (var i = 0; i < values.Length; i++)
            if (i < period - 1)
            {
                movingAverage.Add(double.NaN);
            }
            else
            {
                var avg = values.Skip(i - (period - 1)).Take(period).Average();
                movingAverage.Add(avg);
            }

        return movingAverage;
    }
}