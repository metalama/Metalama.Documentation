using System;
namespace Doc.LAMA0286.Fixed;
internal class OrderService
{
  [LogStartTime]
  public void PlaceOrder(string product)
  {
    object startTime = DateTime.Now;
    Console.WriteLine($"PlaceOrder started at {startTime}.");
  }
}