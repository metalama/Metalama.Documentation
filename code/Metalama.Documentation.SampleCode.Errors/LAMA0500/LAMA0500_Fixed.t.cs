using System;
using System.Collections.Generic;
namespace Doc.LAMA0500.Fixed;
// Fixed: the aspect overrides the existing Reset method.
[Resettable]
internal partial class ShoppingCart
{
  private readonly List<string> _items = new();
  public void Add(string item) => this._items.Add(item);
  public void Reset()
  {
    Console.WriteLine("Resetting the object.");
    this._items.Clear();
  }
}