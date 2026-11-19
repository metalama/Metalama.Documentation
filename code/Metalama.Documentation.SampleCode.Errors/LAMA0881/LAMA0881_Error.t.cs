// Warning LAMA0881 on `Category`: `'LogAttribute' must be written in an immutable style because it is an aspect, a fabric, a validator, or another type bound by the [ImmutableType] contract, but the automatic property 'Category' has a setter that is not private, so not every assignment to it can be seen. Replace 'set' with 'init', make the setter private, or, for a positional record struct, declare it as a 'readonly record struct'.`
using System;
namespace Doc.LAMA0881.Error;
internal class OrderService
{
  [Log(Category = "Orders")]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("[Orders] Entering OrderService.PlaceOrder(string, int).");
  // Places the order.
  }
}