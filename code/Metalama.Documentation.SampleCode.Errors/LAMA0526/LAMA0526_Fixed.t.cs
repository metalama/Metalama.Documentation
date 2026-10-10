using System;
namespace Doc.LAMA0526.Fixed;
// Fixed: the [Indexer] aspect introduces an indexer with an 'int' parameter.
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