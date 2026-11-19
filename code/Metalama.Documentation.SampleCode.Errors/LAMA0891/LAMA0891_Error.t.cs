// Warning LAMA0891 on `Prefix`: `'LogAttribute' is a run-time-or-compile-time type, but the static member 'Prefix' is not read-only. A static field of a compile-time type is shared by every aspect instance, every target and every compilation, and at design time it keeps its value for as long as the analysis process runs. Mark the member with [RunTime] so that it exists only at run time, or make it read-only and of an immutable type.`
using System;
namespace Doc.LAMA0891.Error;
internal class OrderService
{
  [Log]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("Entering OrderService.PlaceOrder(string, int).");
  // Places the order.
  }
}