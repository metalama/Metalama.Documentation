// This is public domain Metalama sample code.

namespace Doc.CompareTaxCalculators;

public class LegacyTaxCalculator
{
    public decimal ComputeTax( decimal amount, string region )
        => region == "EU" ? amount * 0.2m : amount * 0.1m;
}

public static class NewTaxCalculator
{
    public static decimal ComputeTax( decimal amount, string region )
        => region switch
        {
            "EU" => amount * 0.2m,
            "CH" => amount * 0.081m,
            _ => amount * 0.1m
        };
}
