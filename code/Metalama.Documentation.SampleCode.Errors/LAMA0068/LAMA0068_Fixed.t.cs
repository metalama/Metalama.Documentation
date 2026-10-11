using System;
namespace Doc.LAMA0068.Fixed;
internal class OrderService
{
  [Log]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("Executing OrderService.PlaceOrder(string, int).");
  }
}