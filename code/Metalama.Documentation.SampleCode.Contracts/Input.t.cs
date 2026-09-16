using System;
using Metalama.Patterns.Contracts;
namespace Doc.Contracts.Input;
public class Customer
{
  private string? _phone;
  [Phone]
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
  [Url]
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
  [Range(1900, 2100)]
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
  public string? FirstName { get; set; }
  private string _lastName = default !;
  [Required]
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
  public Customer([Required] string fullName)
  {
    if (string.IsNullOrWhiteSpace(fullName))
    {
      if (fullName == null !)
      {
        throw new ArgumentNullException("fullName", "The 'fullName' parameter is required.");
      }
      else
      {
        throw new ArgumentException("The 'fullName' parameter is required.", "fullName");
      }
    }
    var split = fullName.Split(' ');
    if (split.Length == 0)
    {
      this.FirstName = "";
      this.LastName = split[0];
    }
    else
    {
      this.FirstName = split[0];
      this.LastName = split[^1];
    }
  }
}