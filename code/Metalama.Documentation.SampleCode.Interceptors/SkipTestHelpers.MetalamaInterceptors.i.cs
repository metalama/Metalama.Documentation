using System;
using Doc.SkipTestHelpers;
internal static class MetalamaInterceptors
{
  internal static string PaymentGateway_Charge_Interceptor(decimal amount)
  {
    Console.WriteLine("Calling Charge.");
    return PaymentGateway.Charge(amount);
  }
}