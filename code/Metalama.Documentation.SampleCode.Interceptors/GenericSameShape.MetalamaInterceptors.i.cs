using System;
using Doc.GenericSameShape;
internal static class MetalamaInterceptors
{
  internal static void Repository_SaveCustomer_Interceptor(Customer customer)
  {
    if (customer == null)
    {
      throw new ArgumentNullException(nameof(customer));
    }
    Repository.SaveCustomer(customer);
  }
  internal static void Repository_SaveOrder_Interceptor(Order order)
  {
    if (order == null)
    {
      throw new ArgumentNullException(nameof(order));
    }
    Repository.SaveOrder(order);
  }
}