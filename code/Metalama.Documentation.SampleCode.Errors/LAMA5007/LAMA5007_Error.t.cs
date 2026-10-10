// Warning LAMA5007 on `Positive`: `The meaning of the [PositiveAttribute] attribute on Product.Price is ambiguous because the inequality strictness is not specified. It is now interpreted as NonStrict, which is non-standard, and this behavior might be changed in the future. Use either [NonNegativeAttribute] or [StrictlyPositiveAttribute] or specify the DefaultInequalityStrictness property in ContractOptions using the ConfigureContracts fabric extension method.`
using System;
using Metalama.Patterns.Contracts;
namespace Doc.LAMA5007.Error;
public class Product
{
  private decimal _price;
  // Warning: [Positive] doesn't say whether zero is allowed.
  [Positive]
  public decimal Price
  {
    get
    {
      return _price;
    }
    set
    {
      if (value < 0M)
      {
        throw new ArgumentOutOfRangeException("value", value, "The 'Price' property must be greater than or equal to 0.");
      }
      _price = value;
    }
  }
}