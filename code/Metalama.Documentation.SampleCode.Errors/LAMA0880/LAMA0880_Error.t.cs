// Warning LAMA0880 on `Category`: `'LogAttribute' must be written in an immutable style because it is an aspect, a fabric, a validator, or another type bound by the [ImmutableType] contract, but the field 'Category' is neither read-only nor private, so not every assignment to it can be seen. Add the 'readonly' modifier, or make the field private.`
using System;
namespace Doc.LAMA0880.Error;
internal class OrderService
{
  [Log("Orders")]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("[Orders] Entering OrderService.PlaceOrder(string, int).");
  // Places the order.
  }
}