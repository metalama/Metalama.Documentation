// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.LAMA5004.Error;

public partial class Invoice
{
    public decimal Amount { get; set; }

    public decimal Discount { get; set; }

    [Invariant]
    private void CheckDiscount()
    {
        if ( this.Discount > this.Amount )
        {
            throw new InvariantViolationException( "The discount cannot exceed the amount." );
        }
    }

    // Error: invariant suspension is not enabled for Invoice.
    [SuspendInvariants]
    public void Update( decimal amount, decimal discount )
    {
        this.Amount = amount;
        this.Discount = discount;
    }
}
