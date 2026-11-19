// Warning LAMA0886 on `ImmutableType`: `The [ImmutableType] attribute on 'LogAttribute' has no effect, because 'LogAttribute' derives from or implements 'IAspect', which already requires it. Remove the attribute.`
using System;
namespace Doc.LAMA0886.Error;
internal class OrderService
{
  [Log("Orders")]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("[Orders] Entering OrderService.PlaceOrder(string, int).");
  // Places the order.
  }
}