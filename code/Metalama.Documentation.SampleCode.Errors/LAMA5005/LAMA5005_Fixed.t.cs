using Metalama.Patterns.Contracts;
namespace Doc.LAMA5005.Fixed;
public partial class Account
{
  private decimal _balance;
  public decimal Balance
  {
    get
    {
      return _balance;
    }
    set
    {
      try
      {
        _balance = value;
        return;
      }
      finally
      {
        if (!this.AreInvariantsSuspended())
        {
          this.VerifyInvariants();
        }
      }
    }
  }
  private decimal _creditLimit;
  public decimal CreditLimit
  {
    get
    {
      return _creditLimit;
    }
    set
    {
      try
      {
        _creditLimit = value;
        return;
      }
      finally
      {
        if (!this.AreInvariantsSuspended())
        {
          this.VerifyInvariants();
        }
      }
    }
  }
  [SuspendInvariants]
  public void Reset(decimal balance, decimal creditLimit)
  {
    using (this.SuspendInvariants())
    {
      try
      {
        this.CreditLimit = creditLimit;
        this.Balance = balance;
      }
      finally
      {
        if (!this.AreInvariantsSuspended())
        {
          this.VerifyInvariants();
        }
      }
      return;
    }
  }
  // Fixed: the type now has an invariant that [SuspendInvariants] can suspend.
  [Invariant]
  private void CheckBalance()
  {
    if (this.Balance < -this.CreditLimit)
    {
      throw new InvariantViolationException("The balance exceeds the credit limit.");
    }
  }
  private readonly InvariantSuspensionCounter _invariantSuspensionCounter = new();
  protected bool AreInvariantsSuspended()
  {
    return _invariantSuspensionCounter.AreInvariantsSuspended;
  }
  protected SuspendInvariantsCookie SuspendInvariants()
  {
    _invariantSuspensionCounter.Increment();
    return new SuspendInvariantsCookie(_invariantSuspensionCounter);
  }
  protected virtual void VerifyInvariants()
  {
    CheckBalance();
  }
}