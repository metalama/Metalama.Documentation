// Warning LAMA0652 on `Price`: `The original implementation of 'Product.Equals(Product?)' generated for record 'Product' reads the property 'Product.Price', whereas the C# compiler reads its backing field. The backing field of an auto-property cannot be read from source code. The two implementations differ when a derived type overrides the property. Declare the property explicitly with a backing field, or make it non-overridable.`
using System;
using System.Collections.Generic;
namespace Doc.LAMA0652.Error;
[TrackEquality]
public record Product
{
  public string Name { get; init; } = "";
  // The property is virtual, so a derived type can override it.
  public virtual decimal Price { get; set; }
  public virtual bool Equals(Product? other)
  {
    Console.WriteLine("Comparing records.");
    return (object)this == (object? )other || ((object? )other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.Name, other.Name) && EqualityComparer<decimal>.Default.Equals(this.Price, other.Price));
  }
}