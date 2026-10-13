using System;
using System.IO;
namespace Doc.CallerLogger;
[LogPaymentCalls]
public class OrderService
{
  private static readonly TextWriter _log = Console.Out;
  public string Checkout(decimal amount) => PaymentGateway_Charge_Interceptor(amount: amount, currency: "EUR");
  private static string PaymentGateway_Charge_Interceptor(decimal amount, string currency)
  {
    _log.WriteLine("Calling Charge.");
    return PaymentGateway.Charge(amount, currency);
  }
}
public static class PaymentGateway
{
  public static string Charge(decimal amount, string currency) => $"ch_{amount}_{currency}";
}
internal class Program
{
  private static void Main() => new OrderService().Checkout(42);
}