// Warning LAMA0654 on `Price`: `The original implementation of 'Product.Equals(Product?)' generated for record 'Product' reads the property 'Product.Price', whereas the C# compiler reads its backing field. The property is declared with the 'field' keyword and its getter has a body, so the getter can return a value other than the one that the backing field holds, and the two implementations then differ. Declare the property explicitly with a backing field, or give it an automatic getter.`
using System;
using System.Collections.Generic;
namespace Doc.LAMA0654.Error;
[LogEquals]
public record Product(string Name)
{
  // The getter has a body, so the original Equals reads the backing field but the overridden one reads the property.
  public double Price { get => Math.Round(field, 2); set => field = value; }
  public virtual bool Equals(Product? other)
  {
    Console.WriteLine("Comparing two records.");
    return (object)this == (object? )other || ((object? )other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.Name, other.Name) && EqualityComparer<double>.Default.Equals(this.Price, other.Price));
  }
}