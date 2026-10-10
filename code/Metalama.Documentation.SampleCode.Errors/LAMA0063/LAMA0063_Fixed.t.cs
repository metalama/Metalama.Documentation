using System;
namespace Doc.LAMA0063.Fixed;
public class Order
{
  public int Quantity { get; set; }
  public void Validate()
  {
    if (this.Quantity <= 0)
    {
      throw new InvalidOperationException("The quantity must be positive.");
    }
  }
}
public class OrderService
{
  [ValidateFirst]
  public void Submit(Order order)
  {
    order.Validate();
    Console.WriteLine($"Submitting an order of {order.Quantity} items.");
  }
}