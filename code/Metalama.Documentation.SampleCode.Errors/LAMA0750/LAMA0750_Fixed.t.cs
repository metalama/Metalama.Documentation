namespace Doc.LAMA0750.Fixed;
public enum OrderStatus
{
  Pending,
  Shipped,
  Delivered
}
// Fixed: the aspect is applied to a class, which can receive introduced members.
[TypeName]
public partial class Order
{
  public OrderStatus Status { get; set; }
  public string GetTypeName()
  {
    return "Order";
  }
}