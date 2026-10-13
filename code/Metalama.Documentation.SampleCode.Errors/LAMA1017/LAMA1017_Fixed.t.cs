namespace Doc.LAMA1017.Fixed;
[InterceptProcess]
public class OrderService
{
  private sealed class Order
  {
  }
  private static void Process(Order order)
  {
  }
  public static void Run() => OrderService_Process_Interceptor(order: new Order());
  private static void OrderService_Process_Interceptor(Order order)
  {
    Process(order);
  }
}