using System;
using Metalama.Patterns.Contracts;
namespace Doc.RequiredContract;
public class Instrument
{
  private string _name = default !;
  [Required]
  public string Name
  {
    get
    {
      return _name;
    }
    set
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        if (value == null !)
        {
          throw new ArgumentNullException("value", "The 'Name' property is required.");
        }
        else
        {
          throw new ArgumentException("The 'Name' property is required.", "value");
        }
      }
      _name = value;
    }
  }
  private Category _category = default !;
  [Required]
  public Category Category
  {
    get
    {
      return _category;
    }
    set
    {
      if (value == null !)
      {
        throw new ArgumentNullException("value", "The 'Category' property is required.");
      }
      _category = value;
    }
  }
  public Instrument([Required] string name, [Required] Category category)
  {
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
    if (category == null !)
    {
      throw new ArgumentNullException("category", "The 'category' parameter is required.");
    }
    this.Name = name;
    this.Category = category;
  }
}
public class Category
{
}