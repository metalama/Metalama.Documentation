using System;
namespace Doc.CompileTimeRules_Immutable;
public class OrderService
{
  [Log(Category = "Orders")]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("[Orders] Executing OrderService.PlaceOrder(string, int).");
  }
}