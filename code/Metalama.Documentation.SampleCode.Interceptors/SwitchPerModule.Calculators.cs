// This is public domain Metalama sample code.

namespace Doc.SwitchPerModule;

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

    // Adapts the calls written for the legacy calculator. The calculator instance is not used.
    public static decimal ComputeTax( LegacyTaxCalculator calculator, decimal amount, string region )
        => ComputeTax( amount, region );
}
