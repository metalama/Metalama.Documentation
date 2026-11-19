// Warning LAMA0884 on `_categories`: `'LogAttribute' must be written in an immutable style because it is an aspect, a fabric, a validator, or another type bound by the [ImmutableType] contract, but 'IReadOnlyList<string>' is not marked [ImmutableType]. Marking it requires every implementation to be immutable, which Metalama verifies. Path: LogAttribute -> _categories -> IReadOnlyList<string> (an interface or abstract type that is not marked [ImmutableType]).`
using System;
namespace Doc.LAMA0884.Error;
internal class OrderService
{
  [Log("Orders", "Billing")]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("[Orders, Billing] Entering OrderService.PlaceOrder(string, int).");
  // Places the order.
  }
}