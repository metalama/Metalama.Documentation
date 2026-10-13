// This is public domain Metalama sample code.

using System;

namespace Doc.TraceCalls;

// A third-party library, compiled in another project.
public class PricingClient
{
    public decimal GetPrice( string sku, int quantity )
        => sku switch
        {
            "BOOK" => 12.5m * quantity,
            _ => throw new ArgumentException( $"Unknown SKU '{sku}'." )
        };

    public string GetCurrency( string country ) => country == "US" ? "USD" : "EUR";
}
