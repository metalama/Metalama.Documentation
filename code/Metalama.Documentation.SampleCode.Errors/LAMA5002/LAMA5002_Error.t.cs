// Warning LAMA5002 on `Email`: `The [NotNull] contract has been applied to 'Customer.Email', but its type is nullable.`
using System;
using Metalama.Patterns.Contracts;
namespace Doc.LAMA5002.Error;
public class Customer
{
  private string? _email;
  // Warning: the [NotNull] contract contradicts the nullable type.
  [NotNull]
  public string? Email
  {
    get
    {
      return _email;
    }
    set
    {
      if (value == null !)
      {
        throw new ArgumentNullException("value", "The 'Email' property must not be null.");
      }
      _email = value;
    }
  }
}