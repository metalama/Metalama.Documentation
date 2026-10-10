using System;
namespace Doc.LAMA0040.Fixed;
internal class OrderService
{
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("Executing OrderService.PlaceOrder(string, int).");
  }
  public void CancelOrder(int orderId)
  {
    Console.WriteLine("Executing OrderService.CancelOrder(int).");
  }
}