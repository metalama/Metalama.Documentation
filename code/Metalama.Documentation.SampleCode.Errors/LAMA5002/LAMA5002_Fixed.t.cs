using System;
using Metalama.Patterns.Contracts;
namespace Doc.LAMA5002.Fixed;
public class Customer
{
  private string _email = "";
  // Fixed: the type is non-nullable, consistently with the [NotNull] contract.
  [NotNull]
  public string Email
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