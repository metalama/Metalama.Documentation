using System;
using Metalama.Patterns.Contracts;
namespace Doc.CreditCardContract;
public class Customer
{
  private string? _creditCard;
  [CreditCard]
  public string? CreditCard
  {
    get
    {
      return _creditCard;
    }
    set
    {
      if (!ContractHelpers.IsValidCreditCardNumber(value))
      {
        throw new ArgumentException("The 'CreditCard' property must be a valid credit card number.", "value");
      }
      _creditCard = value;
    }
  }
}