namespace Doc.LAMA0512.Fixed;
// Fixed: the aspect ignores Product because it already implements IDescribable.
[Describable]
internal partial class Product : IDescribable
{
  public string Describe() => "A product";
}
[Describable]
internal partial class Customer : IDescribable
{
  public string Describe()
  {
    return "Customer";
  }
}