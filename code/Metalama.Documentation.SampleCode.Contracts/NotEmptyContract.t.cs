using System;
using Metalama.Patterns.Contracts;
namespace Doc.NotEmptyContract;
public class Instrument
{
  private string _name = default !;
  // Neither null nor empty strings are allowed.
  [NotNull]
  [NotEmpty]
  public string Name
  {
    get
    {
      return _name;
    }
    set
    {
      if (value == null !)
      {
        throw new ArgumentNullException("value", "The 'Name' property must not be null.");
      }
      if (value.Length <= 0)
      {
        throw new ArgumentException("The 'Name' property must not be null or empty.", "value");
      }
      _name = value;
    }
  }
  private string? _description;
  // Null strings are allowed but not empty strings.
  [NotEmpty]
  public string? Description
  {
    get
    {
      return _description;
    }
    set
    {
      if (value != null && value.Length <= 0)
      {
        throw new ArgumentException("The 'Description' property must not be null or empty.", "value");
      }
      _description = value;
    }
  }
  private string _currency = default !;
  // Equivalent to [NotNull, NotEmpty]
  [Required]
  public string Currency
  {
    get
    {
      return _currency;
    }
    set
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        if (value == null !)
        {
          throw new ArgumentNullException("value", "The 'Currency' property is required.");
        }
        else
        {
          throw new ArgumentException("The 'Currency' property is required.", "value");
        }
      }
      _currency = value;
    }
  }
  public Instrument(string name, string currency, string? description)
  {
    this.Name = name;
    this.Description = description;
    this.Currency = currency;
  }
}