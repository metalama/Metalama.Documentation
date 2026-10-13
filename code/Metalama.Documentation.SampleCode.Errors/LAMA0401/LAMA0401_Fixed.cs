// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0401.Fixed;

// Fixed: the attribute is available at compile time, so the [Log] aspect can instantiate it.
[AttributeUsage( AttributeTargets.Method )]
[RunTimeOrCompileTime]
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
