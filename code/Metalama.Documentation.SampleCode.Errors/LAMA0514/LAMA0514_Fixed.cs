// This is public domain Metalama sample code.

namespace Doc.LAMA0514.Fixed;

[Describable]
internal partial class Order
{
    public int Id { get; set; }

    // Fixed: this method implements IDescribable.Describe.
    public string Describe() => $"Order {this.Id}";
}
