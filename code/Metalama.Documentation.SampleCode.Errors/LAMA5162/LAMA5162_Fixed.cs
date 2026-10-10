// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5162.Fixed;

[Observable]
public partial class Order
{
    public decimal Subtotal { get; set; }

    public decimal Total => this.ApplyDiscount( this.Subtotal );

    // Fixed: the result depends only on the argument, so the method is marked as constant.
    [Constant]
    private decimal ApplyDiscount( decimal amount ) => amount > 100 ? amount * 0.9m : amount;
}
