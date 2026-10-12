using System;
using System.Linq;
namespace Doc.LAMA0267.Fixed;
internal class OrderService
{
  [LogCategory]
  public void PlaceOrder(int productId)
  {
    var categories = new[]
    {
      "Orders",
      "Billing"
    };
    Func<string> getCategory = () => categories.First();
    Console.WriteLine($"[{getCategory()}] Executing PlaceOrder.");
  }
}