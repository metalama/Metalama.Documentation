using System;
namespace Doc.TupleInterceptor;
public class Invoice
{
  [Intercept]
  public void AddProduct(string productCode, decimal quantity)
  {
    Interceptor.Intercept(AddProductImpl, (productCode, quantity));
  }
  private void AddProduct_Source(string productCode, decimal quantity)
  {
    Console.WriteLine($"Adding {quantity}x {productCode}.");
  }
  [Intercept]
  public void Cancel()
  {
    Interceptor.Intercept(CancelImpl, ValueTuple.Create());
  }
  private void Cancel_Source()
  {
    Console.WriteLine($"Cancelling the order.");
  }
  private void AddProductImpl((string productCode, decimal quantity) args)
  {
    AddProduct_Source(args.productCode, args.quantity);
  }
  private void CancelImpl(ValueTuple args)
  {
    Cancel_Source();
  }
}
public static class Interceptor
{
  // Interceptor method expecting a single payload parameter.
  public static void Intercept<T>(Action<T> action, in T args)
  {
    Console.WriteLine("Intercepted!");
    action(args);
  }
}