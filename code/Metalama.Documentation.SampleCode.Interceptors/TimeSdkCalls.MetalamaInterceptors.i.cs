using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Doc.TimeSdkCalls;
internal static class MetalamaInterceptors
{
  internal static string PaymentGateway_Charge_Interceptor(PaymentGateway receiver, decimal amount, string currency)
  {
    var stopwatch = Stopwatch.StartNew();
    try
    {
      return receiver.Charge(amount, currency);
    }
    finally
    {
      Console.WriteLine($"Charge took {stopwatch.ElapsedMilliseconds} ms.");
    }
  }
  internal static decimal PaymentGateway_GetBalance_Interceptor(PaymentGateway receiver)
  {
    var stopwatch = Stopwatch.StartNew();
    try
    {
      return receiver.GetBalance();
    }
    finally
    {
      Console.WriteLine($"GetBalance took {stopwatch.ElapsedMilliseconds} ms.");
    }
  }
  internal static async Task PaymentGateway_RefundAsync_Interceptor(PaymentGateway receiver, string chargeId)
  {
    var stopwatch = Stopwatch.StartNew();
    try
    {
      await receiver.RefundAsync(chargeId);
      return;
    }
    finally
    {
      Console.WriteLine($"RefundAsync took {stopwatch.ElapsedMilliseconds} ms.");
    }
  }
}