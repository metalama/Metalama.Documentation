// Warning LAMA0890 on `ExcludedMethods`: `'LogSettings' is a compile-time type, but the static member 'ExcludedMethods' is of type 'List<string>', which is not immutable. A static field of a compile-time type is shared by every aspect instance, every target and every compilation, and at design time it keeps its value for as long as the analysis process runs. Path: LogSettings -> ExcludedMethods -> List<string> (a mutable collection; use the corresponding type of System.Collections.Immutable).`
using System;
namespace Doc.LAMA0890.Error;
[Log]
internal class OrderService : IDisposable
{
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("Entering OrderService.PlaceOrder(string, int).");
  // Places the order.
  }
  public void Dispose()
  {
  }
}