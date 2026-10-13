namespace Doc.TemplateAddedParameter;
[TracePricing]
public class Cart
{
  public decimal Total { get; private set; }
  public void Add(string sku, int quantity) => this.Total += MetalamaInterceptors.PricingClient_GetPrice_Interceptor(sku: sku, quantity: quantity, category: "Pricing", caller: "Add");
  public void AddGift(string sku) => this.Total += MetalamaInterceptors.PricingClient_GetPrice_Interceptor(sku: sku, quantity: 1, category: "Pricing", caller: "AddGift") * 0.5m;
}
public static class PricingClient
{
  public static decimal GetPrice(string sku, int quantity) => 10m * quantity;
}