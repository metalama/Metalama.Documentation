// This is public domain Metalama sample code.

namespace Doc.TemplateAddedParameter;

[TracePricing]
public class Cart
{
    public decimal Total { get; private set; }

    public void Add( string sku, int quantity )
        => this.Total += PricingClient.GetPrice( sku, quantity );

    public void AddGift( string sku ) => this.Total += PricingClient.GetPrice( sku, 1 ) * 0.5m;
}

public static class PricingClient
{
    public static decimal GetPrice( string sku, int quantity ) => 10m * quantity;
}
