using System;
using Metalama.Patterns.Contracts;
namespace Doc.LAMA5006.Fixed;
public class OrderLine
{
  // Fixed: the uint type already excludes negative values, so the contract is removed.
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