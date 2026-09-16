using Metalama.Patterns.Contracts;
using System;
using System.Collections.Generic;
namespace Doc.NotEmptyCollectionContract;
public class Submenu
{
  public string Name { get; }
  public IReadOnlyCollection<MenuItem> Items { get; }
  public Submenu([Required] string name, [NotNull][NotEmpty] IReadOnlyCollection<MenuItem> items)
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
    if (items == null !)
    {
      throw new ArgumentNullException("items", "The 'items' parameter must not be null.");
    }
    if (items.Count <= 0)
    {
      throw new ArgumentException("The 'items' parameter must not be null or empty.", "items");
    }
    this.Name = name;
    this.Items = items;
  }
}
public record MenuItem(string Name);