using System;
using System.IO;
namespace Doc.LAMA0259.Fixed;
internal class OrderService
{
  private readonly TextWriter _log = Console.Out;
  [Log]
  public void PlaceOrder(string product)
  {
    var log = _log;
    log?.WriteLine("Executing OrderService.PlaceOrder(string).");
    Console.WriteLine($"Ordered {product}.");
  }
}