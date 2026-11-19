// Warning LAMA0888 on `this._prefix`: `'LogAttribute' must be written in an immutable style because it is an aspect, a fabric, a validator, or another type bound by the [ImmutableType] contract, but '_prefix' is passed as an 'out' argument here, which writes it. Pass a local instead, and assign the member in a constructor.`
using System;
namespace Doc.LAMA0888.Error;
internal class OrderService
{
  [Log]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("Entering OrderService.PlaceOrder(string, int).");
  // Places the order.
  }
}