namespace Doc.LAMA0513.Fixed;
// Fixed: the aspect implements IEntity<Product>.
[Entity]
internal partial class Product : IEntity<Product>
{
  public decimal Price { get; set; }
}