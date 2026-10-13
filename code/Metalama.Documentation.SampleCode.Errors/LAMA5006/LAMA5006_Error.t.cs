// Warning LAMA5006 on `Quantity`: `The [NonNegative] contract is redundant on 'OrderLine.Quantity' because the value range [0, ∞] is always satisfied by the type UInt32.`
using System;
using Metalama.Patterns.Contracts;
namespace Doc.LAMA5006.Error;
public class OrderLine
{
  // Warning: a uint value can never be negative.
  [NonNegative]
  public uint Quantity { get; set; }
  private decimal _unitPrice;
  [NonNegative]
  public decimal UnitPrice
  {
    get
    {
      return _unitPrice;
    }
    set
    {
      if (value < 0M)
      {
        throw new ArgumentOutOfRangeException("value", value, "The 'UnitPrice' property must be greater than or equal to 0.");
      }
      _unitPrice = value;
    }
  }
}