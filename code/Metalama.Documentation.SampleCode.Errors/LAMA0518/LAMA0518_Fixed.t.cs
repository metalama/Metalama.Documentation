namespace Doc.LAMA0518.Fixed;
[Describable]
internal partial class Order : IDescribable
{
  public int Id { get; set; }
  string IDescribable.Description
  {
    get
    {
      return "Order";
    }
  }
}