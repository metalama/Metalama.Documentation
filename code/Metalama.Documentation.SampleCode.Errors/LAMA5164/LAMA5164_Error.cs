// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5164.Error;

[Observable]
public partial class OrderLine
{
    public decimal UnitPrice;

    public int Quantity { get; set; }

    // Warning: UnitPrice is a public field, so changes to it can't be observed.
    public decimal Total => this.UnitPrice * this.Quantity;
}
