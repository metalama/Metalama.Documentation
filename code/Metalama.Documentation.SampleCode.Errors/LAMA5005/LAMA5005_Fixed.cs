// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.LAMA5005.Fixed;

public partial class Account
{
    public decimal Balance { get; set; }

    public decimal CreditLimit { get; set; }

    [SuspendInvariants]
    public void Reset( decimal balance, decimal creditLimit )
    {
        this.CreditLimit = creditLimit;
        this.Balance = balance;
    }

    // Fixed: the type now has an invariant that [SuspendInvariants] can suspend.
    [Invariant]
    private void CheckBalance()
    {
        if ( this.Balance < -this.CreditLimit )
        {
            throw new InvariantViolationException( "The balance exceeds the credit limit." );
        }
    }
}
