// This is public domain Metalama sample code.

namespace Doc.LAMA0514.Error;

[Describable]
internal partial class Order
{
    public int Id { get; set; }

    // Error: Describe has the same signature as IDescribable.Describe.
    public string Describe() => $"Order {this.Id}";
}
