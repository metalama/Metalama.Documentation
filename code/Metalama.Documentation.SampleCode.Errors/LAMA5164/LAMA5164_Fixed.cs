// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5164.Fixed;

[Observable]
public partial class OrderLine
{
    // Fixed: the public field is replaced with an auto-property.
    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal Total => this.UnitPrice * this.Quantity;
}
