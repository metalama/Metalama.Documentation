using System;
namespace Doc.NotNullFabric;
public class Instrument
{
  private string _name = default !;
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
  public Category? Category { get; set; }
  public Instrument(string name, Category? category)
  {
    if (name == null !)
    {
      throw new ArgumentNullException("name", "The 'name' parameter must not be null.");
    }
    this.Name = name;
    this.Category = category;
  }
}
public class Category
{
  // Internal APIs won't be checked by default.
  internal Category(string name)
  {
    this.Name = name;
  }
  public string Name { get; }
}