using System;
namespace Doc.LAMA0045.Fixed;
internal class OrderService
{
  [Log(Category = "Orders")]
  public void PlaceOrder(string customerId)
  {
    Console.WriteLine("[Orders] Executing OrderService.PlaceOrder(string).");
  }
}