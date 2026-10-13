namespace Doc.TraceCalls;
public class Cart
{
  private readonly PricingClient _pricing = new();
  public string Currency { get; private set; } = "EUR";
  public decimal Total { get; private set; }
  public void SetCountry(string country) => this.Currency = MetalamaInterceptors.PricingClient_GetCurrency_Interceptor(this._pricing, country: country);
  public void Add(string sku, int quantity) => this.Total += MetalamaInterceptors.PricingClient_GetPrice_Interceptor(this._pricing, sku: sku, quantity: quantity);
}