namespace Doc.LAMA0511.Fixed;
// Fixed: the Describe() template of the aspect has the same return type as IDescribable.Describe().
[Describable]
internal partial class Product : IDescribable
{
  public decimal Price { get; set; }
  public string Describe()
  {
    return "Product";
  }
}