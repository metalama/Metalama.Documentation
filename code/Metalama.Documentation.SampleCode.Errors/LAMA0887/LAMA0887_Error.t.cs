// Warning LAMA0887 on `this._methodName`: `'LogAttribute' must be written in an immutable style because it is an aspect, a fabric, a validator, or another type bound by the [ImmutableType] contract, but '_methodName' is assigned here, outside a constructor. Move the assignment into a constructor or an 'init' accessor.`
using System;
namespace Doc.LAMA0887.Error;
internal class OrderService
{
  [Log]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("Entering PlaceOrder.");
  // Places the order.
  }
}