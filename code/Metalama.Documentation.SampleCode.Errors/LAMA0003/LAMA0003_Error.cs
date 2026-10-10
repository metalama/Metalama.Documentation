// This is public domain Metalama sample code.

namespace Doc.LAMA0003.Error;

internal partial class Invoice
{
    public decimal Amount { get; set; }

    // Error: [Describe] is a type aspect, but it's applied to a method.
    [Describe]
    public decimal GetTotal( decimal taxRate ) => this.Amount * (1 + taxRate);
}
