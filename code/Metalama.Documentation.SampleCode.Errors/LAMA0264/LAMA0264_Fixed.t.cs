using System;
namespace Doc.LAMA0264.Fixed;
[AddLogMethod]
internal partial class OrderService
{
  public void PlaceOrder(int productId)
  {
  }
  public void Log(string message)
  {
    Console.WriteLine($"OrderService: {message}");
  }
}