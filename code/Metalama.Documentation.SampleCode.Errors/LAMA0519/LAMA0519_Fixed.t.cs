namespace Doc.LAMA0519.Fixed;
[Describable]
internal partial class Order : IDescribable
{
  public int Id { get; set; }
  public string Describe()
  {
    return "This is an instance of Order.";
  }
}