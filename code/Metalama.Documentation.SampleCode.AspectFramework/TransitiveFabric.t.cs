using System;
namespace Doc.TransitiveFabric;
// This project references the assembly containing LoggingPolicyFabric.
// The transitive fabric automatically applies logging to all public methods.
public class OrderService
{
  public void PlaceOrder(string orderId)
  {
    Console.WriteLine("Entering OrderService.PlaceOrder(string)");
    try
    {
      Console.WriteLine($"Processing order {orderId}.");
      return;
    }
    finally
    {
      Console.WriteLine("Leaving OrderService.PlaceOrder(string)");
    }
  }
  public void CancelOrder(string orderId)
  {
    Console.WriteLine("Entering OrderService.CancelOrder(string)");
    try
    {
      Console.WriteLine($"Cancelling order {orderId}.");
      return;
    }
    finally
    {
      Console.WriteLine("Leaving OrderService.CancelOrder(string)");
    }
  }
  private void ValidateOrder(string orderId)
  {
  // Private methods are not logged.
  }
}
