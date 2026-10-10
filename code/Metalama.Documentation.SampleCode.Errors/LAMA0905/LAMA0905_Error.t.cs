// Warning LAMA0905 on `new Order()`: `The 'Order' type cannot be referenced by the 'OrderController' type. Use OrderFactory to create orders.`
using Metalama.Extensions.Architecture.Aspects;
using System;
namespace Doc.LAMA0905.Error;
[CanOnlyBeUsedFrom(Types = [typeof(OrderFactory)], Description = "Use OrderFactory to create orders.")]
public class Order
{
  public decimal Amount { get; set; }
}
public class OrderFactory
{
  public Order Create(decimal amount) => new Order
  {
    Amount = amount
  };
}
public class OrderController
{
  public void Submit(decimal amount)
  {
    // OrderController is not allowed to use the Order class.
    Console.WriteLine(new Order());
  }
}