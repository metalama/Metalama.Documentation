// This is public domain Metalama sample code.

namespace Doc.LAMA0042.Error;

// Error: the default order places the weaver of [MakeVirtual] between the two layers of [NotNull].
[MakeVirtual]
internal class OrderService
{
    [Log]
    public string Process( [NotNull] string orderId ) => orderId;
}