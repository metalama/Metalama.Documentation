using System;
using System.Threading.Tasks;
using Doc.RetryShippingCalls;
internal static class MetalamaInterceptors
{
  internal static decimal ShippingClient_GetQuote_Interceptor(ShippingClient receiver, string destination)
  {
    for (var attempt = 1;; attempt++)
    {
      try
      {
        return receiver.GetQuote(destination);
      }
      catch (TimeoutException e)when (attempt < 3)
      {
        Console.WriteLine($"Attempt {attempt} failed: {e.Message} Retrying.");
      }
    }
  }
  internal static async Task<string> ShippingClient_GetTrackingStatusAsync_Interceptor(ShippingClient receiver, string trackingNumber)
  {
    for (var attempt = 1;; attempt++)
    {
      try
      {
        return await receiver.GetTrackingStatusAsync(trackingNumber);
      }
      catch (TimeoutException e)when (attempt < 3)
      {
        Console.WriteLine($"Attempt {attempt} failed: {e.Message} Retrying.");
      }
    }
  }
}