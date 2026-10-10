namespace Doc.LAMA0516.Fixed;
[Describable]
internal partial class Order : IDescribable
{
  public int Id { get; set; }
  public string Describe()
  {
    return "Order";
  }
}