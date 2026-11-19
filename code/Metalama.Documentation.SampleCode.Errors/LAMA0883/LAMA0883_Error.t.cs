// Warning LAMA0883 on `LogAttribute`: `'LogAttribute' must be written in an immutable style because it is an aspect, a fabric, a validator, or another type bound by the [ImmutableType] contract, but its base type 'CategorizedAttribute' is not immutable. Path: LogAttribute -> CategorizedAttribute (an interface or abstract type that is not marked [ImmutableType]).`
using System;
namespace Doc.LAMA0883.Error;
internal class OrderService
{
  [Log("Orders")]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("[Orders] Entering OrderService.PlaceOrder(string, int).");
  // Places the order.
  }
}