// This is public domain Metalama sample code.

using System;
using System.IO;

namespace Doc.CallerLogger;

[LogPaymentCalls]
public class OrderService
{
    private readonly TextWriter _log;

    public OrderService( TextWriter log )
    {
        this._log = log;
    }

    public string Checkout( decimal amount ) => PaymentGateway.Charge( amount, "EUR" );
}

public static class PaymentGateway
{
    public static string Charge( decimal amount, string currency ) => $"ch_{amount}_{currency}";
}

internal class Program
{
    private static void Main() => new OrderService( Console.Out ).Checkout( 42 );
}
