namespace Doc.LAMA0514.Fixed;
[Describable]
internal partial class Order : IDescribable
{
  public int Id { get; set; }
  // Fixed: this method implements IDescribable.Describe.
  public string Describe() => $"Order {this.Id}";
}