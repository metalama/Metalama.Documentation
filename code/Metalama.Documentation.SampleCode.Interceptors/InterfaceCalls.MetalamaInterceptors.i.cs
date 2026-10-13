using System;
using Doc.InterfaceCalls;
internal static class MetalamaInterceptors
{
  internal static void IOrderRepository_Save_Interceptor(IOrderRepository receiver, Order order)
  {
    Console.WriteLine("Calling IOrderRepository.Save(Order).");
    receiver.Save(order);
  }
  internal static void SqlOrderRepository_Save_Interceptor(SqlOrderRepository receiver, Order order)
  {
    Console.WriteLine("Calling SqlOrderRepository.Save(Order).");
    receiver.Save(order);
  }
}