using System;
using System.Collections.Generic;
using Doc.TraceCalls;
internal static class MetalamaInterceptors
{
  internal static string PricingClient_GetCurrency_Interceptor(PricingClient receiver, string country)
  {
    var arguments = new List<string>();
    arguments.Add($"country: {country}");
    var call = "SetCountry -> " + $"GetCurrency({string.Join(", ", arguments)})";
    try
    {
      var result = receiver.GetCurrency(country);
      Console.WriteLine($"{call} returned {result}.");
      return result;
    }
    catch (Exception e)
    {
      Console.WriteLine($"{call} threw {e.GetType().Name}: {e.Message}");
      throw;
    }
  }
  internal static decimal PricingClient_GetPrice_Interceptor(PricingClient receiver, string sku, int quantity)
  {
    var arguments = new List<string>();
    arguments.Add($"sku: {sku}");
    arguments.Add($"quantity: {quantity}");
    var call = "Add -> " + $"GetPrice({string.Join(", ", arguments)})";
    try
    {
      var result = receiver.GetPrice(sku, quantity);
      Console.WriteLine($"{call} returned {result}.");
      return result;
    }
    catch (Exception e)
    {
      Console.WriteLine($"{call} threw {e.GetType().Name}: {e.Message}");
      throw;
    }
  }
}