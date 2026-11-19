// Warning LAMA0882 on `_categories`: `'LogAttribute' must be written in an immutable style because it is an aspect, a fabric, a validator, or another type bound by the [ImmutableType] contract, but 'List<string>' is not immutable. Path: LogAttribute -> _categories -> List<string> (a mutable collection; use the corresponding type of System.Collections.Immutable).`
using System;
namespace Doc.LAMA0882.Error;
internal class OrderService
{
  [Log("Orders", "Billing")]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("[Orders, Billing] Entering OrderService.PlaceOrder(string, int).");
  // Places the order.
  }
}