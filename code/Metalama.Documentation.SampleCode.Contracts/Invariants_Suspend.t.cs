using System;
using Metalama.Patterns.Contracts;
namespace Doc.Invariants_Suspend;
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
  private int _discountPercent;
  [Range(0, 100)]
  public int DiscountPercent
  {
    get
    {
      return _discountPercent;
    }
    set
    {
      try
      {
        if (value is < 0 or > 100)
        {
          throw new ArgumentOutOfRangeException("value", value, "The 'DiscountPercent' property must be in the range [0, 100].");
        }
        _discountPercent = value;
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
  private decimal _discountAmount;
  [Range(0, 100)]
  public decimal DiscountAmount
  {
    get
    {
      return _discountAmount;
    }
    set
    {
      try
      {
        if (value is < 0M or > 100M)
        {
          throw new ArgumentOutOfRangeException("value", value, "The 'DiscountAmount' property must be in the range [0, 100].");
        }
        _discountAmount = value;
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
  public virtual decimal DiscountedAmount => (this.Amount * (100 - this.Amount) / 100m) - this.DiscountAmount;
  [Invariant]
  private void CheckDiscounts()
  {
    if (this.DiscountedAmount < 0)
    {
      throw new PostconditionViolationException("The discounted amount cannot be negative.");
    }
  }
  [SuspendInvariants]
  public void UpdateDiscounts1(int percent, decimal amount)
  {
    using (this.SuspendInvariants())
    {
      try
      {
        this.DiscountAmount = amount;
        this.DiscountPercent = percent;
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
  public void UpdateDiscounts2(int percent, decimal amount)
  {
    try
    {
      using (this.SuspendInvariants())
      {
        this.DiscountAmount = amount;
        this.DiscountPercent = percent;
      }
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
    CheckDiscounts();
  }
}