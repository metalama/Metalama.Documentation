// Warning LAMA1012 on `Slot`: `The call to 'Store.Slot()' cannot be intercepted by the interceptor template 'Trace' registered by the aspect [TraceCallsAttribute] applied to 'Inventory' because the method returns by reference. The call site is left unchanged.`
// Warning LAMA1012 on `IsLarge`: `The call to 'Store.IsLarge(int)' cannot be intercepted by the interceptor template 'Trace' registered by the aspect [TraceCallsAttribute] applied to 'Inventory' because it is in a lambda that is converted to an expression tree, which describes the call instead of making it. The call site is left unchanged.`
using System;
using System.Collections.Generic;
using System.Linq;
namespace Doc.UnsupportedCalls;
[TraceCalls]
public class Inventory
{
  public void Reset() => Store.Slot() = 0;
  public IQueryable<int> FindLarge(IQueryable<int> quantities) => quantities.Where(q => Store.IsLarge(q));
  public IEnumerable<int> FindLarge(IEnumerable<int> quantities) => quantities.Where(q => MetalamaInterceptors.Store_IsLarge_Interceptor(quantity: q));
}
public static class Store
{
  private static int _slot;
  // A method that returns by reference cannot be intercepted.
  public static ref int Slot() => ref _slot;
  public static bool IsLarge(int quantity) => quantity > 100;
}