// This is public domain Metalama sample code.

namespace Doc.TraceCalls;

public class Cart
{
    private readonly PricingClient _pricing = new();

    public string Currency { get; private set; } = "EUR";

    public decimal Total { get; private set; }

    public void SetCountry( string country )
        => this.Currency = this._pricing.GetCurrency( country );

    public void Add( string sku, int quantity )
        => this.Total += this._pricing.GetPrice( sku, quantity );
}
