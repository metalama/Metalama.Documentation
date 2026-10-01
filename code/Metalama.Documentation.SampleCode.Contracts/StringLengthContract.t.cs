using System;
using Metalama.Patterns.Contracts;
namespace Doc.StringLengthContract;
public class Customer
{
  private string? _password;
  [StringLength(12, 64)]
  public string? Password
  {
    get
    {
      return _password;
    }
    set
    {
      if (value != null && (value.Length < 12 || value.Length > 64))
      {
        throw new ArgumentException($"The  'Password' property must be a string with length between {12} and {64}.", "value");
      }
      _password = value;
    }
  }
}