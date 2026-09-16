using System;
using Metalama.Patterns.Contracts;
namespace Doc.Contracts.Inheritance;
public interface ICustomer
{
  [Phone]
  string? Phone { get; set; }
  [Url]
  string? Url { get; set; }
  [Range(1900, 2100)]
  int? BirthYear { get; set; }
  [Required]
  string FirstName { get; set; }
  [Required]
  string LastName { get; set; }
}
public class Customer : ICustomer
{
  private string? _phone;
  public string? Phone
  {
    get
    {
      return _phone;
    }
    set
    {
      var regex = ContractHelpers.PhoneRegex;
      if (value != null && !regex.IsMatch(value))
      {
        var regex_1 = regex;
        throw new ArgumentException("The 'Phone' property must be a valid phone number.", "value");
      }
      _phone = value;
    }
  }
  private string? _url;
  public string? Url
  {
    get
    {
      return _url;
    }
    set
    {
      var regex = ContractHelpers.UrlRegex;
      if (value != null && !regex.IsMatch(value))
      {
        var regex_1 = regex;
        throw new ArgumentException("The 'Url' property must be a valid URL.", "value");
      }
      _url = value;
    }
  }
  private int? _birthYear;
  public int? BirthYear
  {
    get
    {
      return _birthYear;
    }
    set
    {
      if (value is < 1900 or > 2100)
      {
        throw new ArgumentOutOfRangeException("value", value, "The 'BirthYear' property must be in the range [1900, 2100].");
      }
      _birthYear = value;
    }
  }
  private string _firstName = default !;
  public string FirstName
  {
    get
    {
      return _firstName;
    }
    set
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        if (value == null !)
        {
          throw new ArgumentNullException("value", "The 'FirstName' property is required.");
        }
        else
        {
          throw new ArgumentException("The 'FirstName' property is required.", "value");
        }
      }
      _firstName = value;
    }
  }
  private string _lastName = default !;
  public string LastName
  {
    get
    {
      return _lastName;
    }
    set
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        if (value == null !)
        {
          throw new ArgumentNullException("value", "The 'LastName' property is required.");
        }
        else
        {
          throw new ArgumentException("The 'LastName' property is required.", "value");
        }
      }
      _lastName = value;
    }
  }
  public Customer([Required] string firstName, [Required] string lastName)
  {
    if (string.IsNullOrWhiteSpace(firstName))
    {
      if (firstName == null !)
      {
        throw new ArgumentNullException("firstName", "The 'firstName' parameter is required.");
      }
      else
      {
        throw new ArgumentException("The 'firstName' parameter is required.", "firstName");
      }
    }
    if (string.IsNullOrWhiteSpace(lastName))
    {
      if (lastName == null !)
      {
        throw new ArgumentNullException("lastName", "The 'lastName' parameter is required.");
      }
      else
      {
        throw new ArgumentException("The 'lastName' parameter is required.", "lastName");
      }
    }
    this.FirstName = firstName;
    this.LastName = lastName;
  }
}