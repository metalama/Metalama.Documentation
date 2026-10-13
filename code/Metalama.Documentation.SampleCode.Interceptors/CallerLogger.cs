// This is public domain Metalama sample code.

using System;
using System.IO;

namespace Doc.CallerLogger;

[LogPaymentCalls]
public class OrderService
{
    private static readonly TextWriter _log = Console.Out;

    public string Checkout( decimal amount ) => PaymentGateway.Charge( amount, "EUR" );
}

public static class PaymentGateway
{
    public static string Charge( decimal amount, string currency ) => $"ch_{amount}_{currency}";
}

internal class Program
{
    private static void Main() => new OrderService().Checkout( 42 );
}
