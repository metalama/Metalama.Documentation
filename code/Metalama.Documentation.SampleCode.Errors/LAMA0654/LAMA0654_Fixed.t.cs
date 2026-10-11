using System;
using System.Collections.Generic;
namespace Doc.LAMA0654.Fixed;
[LogEquals]
public record Product(string Name)
{
  // The getter is automatic, so reading the property is the same as reading the backing field.
  public double Price { get; set => field = Math.Round(value, 2); }
  public virtual bool Equals(Product? other)
  {
    Console.WriteLine("Comparing two records.");
    return (object)this == (object? )other || ((object? )other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.Name, other.Name) && EqualityComparer<double>.Default.Equals(this.Price, other.Price));
  }
}