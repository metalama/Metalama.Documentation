using System;
using Doc.DedupPerMember;
internal static class MetalamaInterceptors
{
  internal static string PaymentGateway_Charge_Interceptor(decimal amount)
  {
    Console.WriteLine("Pay calls Charge.");
    return PaymentGateway.Charge(amount);
  }
  internal static string PaymentGateway_Charge_Interceptor1(decimal amount)
  {
    Console.WriteLine("PayTwice calls Charge.");
    return PaymentGateway.Charge(amount);
  }
  internal static string PaymentGateway_Charge_Interceptor2(decimal amount)
  {
    Console.WriteLine("Renew calls Charge.");
    return PaymentGateway.Charge(amount);
  }
}