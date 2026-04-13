using System;
namespace Doc.RecordInitializer;
[LogConstruction]
public sealed partial record Product
{
  public string Name { get; init; }
  public decimal Price { get; init; }
  public void Deconstruct(out string Name, out decimal Price)
  {
    Name = this.Name;
    Price = this.Price;
  }
  public Product(string Name, decimal Price)
  {
    this.Name = Name;
    this.Price = Price;
    Console.WriteLine("Constructing Product.");
  }
}