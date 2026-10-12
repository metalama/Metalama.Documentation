using System;
namespace Doc.LAMA0236.Fixed;
public class Logger
{
  public void Write(string message) => Console.WriteLine(message);
}
internal partial class OrderService
{
  [Log]
  public void PlaceOrder(int orderId)
  {
    _logger.Write("Entering OrderService.PlaceOrder(int).");
  }
  private readonly Logger _logger = new Logger();
}