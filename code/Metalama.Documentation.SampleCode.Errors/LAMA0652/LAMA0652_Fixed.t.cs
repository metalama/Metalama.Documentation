using System;
using System.Collections.Generic;
namespace Doc.LAMA0652.Fixed;
[TrackEquality]
public record Product
{
  public string Name { get; init; } = "";
  // The property is not virtual.
  public decimal Price { get; set; }
  public virtual bool Equals(Product? other)
  {
    Console.WriteLine("Comparing records.");
    return (object)this == (object? )other || ((object? )other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.Name, other.Name) && EqualityComparer<decimal>.Default.Equals(this.Price, other.Price));
  }
}