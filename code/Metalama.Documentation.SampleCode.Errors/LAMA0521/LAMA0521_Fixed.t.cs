using System.Diagnostics;
namespace Doc.LAMA0521.Fixed;
// The aspect keeps the existing [DebuggerDisplay] attribute.
[DefaultDebuggerDisplay]
[DebuggerDisplay("Product {Name} ({Price})")]
internal class Product
{
  public string Name { get; set; } = "";
  public decimal Price { get; set; }
}
[DefaultDebuggerDisplay]
[DebuggerDisplay("{Name}")]
internal class Customer
{
  public string Name { get; set; } = "";
}