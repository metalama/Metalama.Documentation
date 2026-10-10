// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5162.Error;

[Observable]
public partial class Order
{
    public decimal Subtotal { get; set; }

    // Warning: ApplyDiscount is an instance method of a mutable type, so it isn't known to be constant.
    public decimal Total => this.ApplyDiscount( this.Subtotal );

    private decimal ApplyDiscount( decimal amount ) => amount > 100 ? amount * 0.9m : amount;
}
