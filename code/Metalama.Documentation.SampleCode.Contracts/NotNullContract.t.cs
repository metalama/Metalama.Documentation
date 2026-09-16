using System;
using Metalama.Patterns.Contracts;
namespace Doc.NotNullContract;
public class Instrument
{
  private string _name = default !;
  [NotNull]
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
      _name = value;
    }
  }
  private Category _category = default !;
  [NotNull]
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
        throw new ArgumentNullException("value", "The 'Category' property must not be null.");
      }
      _category = value;
    }
  }
  public Instrument([NotNull] string name, [NotNull] Category category)
  {
    if (name == null !)
    {
      throw new ArgumentNullException("name", "The 'name' parameter must not be null.");
    }
    if (category == null !)
    {
      throw new ArgumentNullException("category", "The 'category' parameter must not be null.");
    }
    this.Name = name;
    this.Category = category;
  }
}
public class Category
{
}