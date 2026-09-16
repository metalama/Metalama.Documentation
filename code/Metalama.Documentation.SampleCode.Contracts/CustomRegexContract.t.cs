using System;
using Metalama.Patterns.Contracts;
namespace Doc.CustomRegexContract;
public class Customer
{
  private string? _password;
  [Password]
  public string? Password
  {
    get
    {
      return _password;
    }
    set
    {
      var regex = ContractHelpers.GetRegex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&#])[A-Za-z\\d@$!%*?&#]{8,20}$\n", 0);
      if (value != null && !regex.IsMatch(value))
      {
        var regex_1 = regex;
        throw new ArgumentException($"The 'Password' property must match the regular expression '{regex_1}'.", "value");
      }
      _password = value;
    }
  }
}