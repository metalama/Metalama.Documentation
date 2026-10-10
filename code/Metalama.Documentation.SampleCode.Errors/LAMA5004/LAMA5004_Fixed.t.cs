using Metalama.Patterns.Contracts;
namespace Doc.LAMA5004.Fixed;
public partial class Invoice
{
  private decimal _amount;
  public decimal Amount
  {
    get
    {
      return _amount;
    }
    set
    {
      try
      {
        _amount = value;
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
  private decimal _discount;
  public decimal Discount
  {
    get
    {
      return _discount;
    }
    set
    {
      try
      {
        _discount = value;
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
  [Invariant]
  private void CheckDiscount()
  {
    if (this.Discount > this.Amount)
    {
      throw new InvariantViolationException("The discount cannot exceed the amount.");
    }
  }
  // Fixed: the fabric enables invariant suspension.
  [SuspendInvariants]
  public void Update(decimal amount, decimal discount)
  {
    using (this.SuspendInvariants())
    {
      try
      {
        this.Amount = amount;
        this.Discount = discount;
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
    CheckDiscount();
  }
}