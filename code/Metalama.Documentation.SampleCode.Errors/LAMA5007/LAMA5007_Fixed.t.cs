using System;
using Metalama.Patterns.Contracts;
namespace Doc.LAMA5007.Fixed;
public class Product
{
  private decimal _price;
  // Fixed: [StrictlyPositive] states explicitly that zero isn't allowed.
  [StrictlyPositive]
  public decimal Price
  {
    get
    {
      return _price;
    }
    set
    {
      if (value <= 0M)
      {
        throw new ArgumentOutOfRangeException("value", value, "The 'Price' property must be strictly greater than 0.");
      }
      _price = value;
    }
  }
}