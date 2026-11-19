// Warning LAMA0889 on `Prefix`: `'LogSettings' is a compile-time type, but the static member 'Prefix' is not read-only. A static field of a compile-time type is shared by every aspect instance, every target and every compilation, and at design time it keeps its value for as long as the analysis process runs. Add the 'readonly' modifier to the field, or remove the setter of the property.`
using System;
namespace Doc.LAMA0889.Error;
internal class OrderService
{
  [Log]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("Entering OrderService.PlaceOrder(string, int).");
  // Places the order.
  }
}