// This is public domain Metalama sample code.

using System;

namespace Doc.LocalVariableTransaction;

public class Account
{
    private decimal _amount;

    public DateTime LastOperation { get; private set; }

    public decimal Amount
    {
        get => this._amount;
        private set
        {
            if ( value < 0 )
            {
                throw new ArgumentOutOfRangeException();
            }

            this._amount = value;
        }
    }

    [TransactedMethod]
    public void Withdraw( decimal amount )
    {
        this.LastOperation = DateTime.Now;
        this.Amount -= amount;
    }
}