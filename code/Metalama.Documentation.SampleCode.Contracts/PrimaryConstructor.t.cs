using System;
using Metalama.Patterns.Contracts;
namespace Doc.Contracts.PrimaryConstructor;
public class Customer
{
  public string Name { get; }
  public string? Phone { get; }
  public Customer([Required] string name, [Phone] string? phone = null)
  {
    Name = name;
    Phone = phone;
    if (string.IsNullOrWhiteSpace(name))
    {
      if (name == null !)
      {
        throw new ArgumentNullException("name", "The 'name' parameter is required.");
      }
      else
      {
        throw new ArgumentException("The 'name' parameter is required.", "name");
      }
    }
    var regex = ContractHelpers.PhoneRegex;
    if (phone != null && !regex.IsMatch(phone))
    {
      var regex_1 = regex;
      throw new ArgumentException("The 'phone' parameter must be a valid phone number.", "phone");
    }
  }
}