// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0401.Error;

// Error: the attribute is run-time-only, so the [Log] aspect can't instantiate it at compile time.
[AttributeUsage( AttributeTargets.Method )]
public class LogCategoryAttribute : Attribute
{
    public LogCategoryAttribute( string category )
    {
        this.Category = category;
    }

    public string Category { get; }

    public override string ToString() => this.Category;
}

internal class InvoiceService
{
    [Log]
    [LogCategory( "Billing" )]
    public void SendInvoice( int invoiceId )
    {
        Console.WriteLine( $"Sending invoice {invoiceId}." );
    }
}
