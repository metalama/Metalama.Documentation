// This is public domain Metalama sample code.

namespace Doc.LAMA0042.Fixed;

// Fixed: an [AspectOrder] attribute orders [MakeVirtual] relatively to [NotNull].
[MakeVirtual]
internal class OrderService
{
    [Log]
    public string Process( [NotNull] string orderId ) => orderId;
}