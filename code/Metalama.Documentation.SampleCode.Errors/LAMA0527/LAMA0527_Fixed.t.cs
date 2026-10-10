using System;
namespace Doc.LAMA0527.Fixed;
// Fixed: the [Indexer] aspect introduces an instance indexer.
[Indexer]
internal partial class ProductCatalog
{
  public object? this[int index]
  {
    get
    {
      Console.WriteLine("Getting an item.");
      return default(object? );
    }
    set
    {
      Console.WriteLine("Setting an item.");
    }
  }
}