using System;
using Metalama.Patterns.Contracts;
namespace Doc.Invariants;
public class Invoice
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
        this.VerifyInvariants();
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
        this.VerifyInvariants();
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
        this.VerifyInvariants();
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
  protected virtual void VerifyInvariants()
  {
    CheckDiscounts();
  }
}