using System;
using Doc.TemplateAddedParameter;
internal static class MetalamaInterceptors
{
  internal static decimal PricingClient_GetPrice_Interceptor(string sku, int quantity, string category, string caller)
  {
    Console.WriteLine($"[{category}] {caller} calls GetPrice.");
    return PricingClient.GetPrice(sku, quantity);
  }
}