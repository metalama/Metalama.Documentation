using System;
namespace Doc.LAMA0042.Fixed;
// Fixed: an [AspectOrder] attribute orders [MakeVirtual] relatively to [NotNull].
[MakeVirtual]
internal class OrderService
{
  [Log]
  public virtual string Process([NotNull] string orderId)
  {
    if (orderId == null)
    {
      throw new ArgumentNullException("orderId");
    }
    Console.WriteLine("Executing OrderService.Process(string).");
    return orderId;
  }
}