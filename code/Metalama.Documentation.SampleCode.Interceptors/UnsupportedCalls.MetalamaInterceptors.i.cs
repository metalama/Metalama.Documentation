using System;
using Doc.UnsupportedCalls;
internal static class MetalamaInterceptors
{
  internal static bool Store_IsLarge_Interceptor(int quantity)
  {
    Console.WriteLine("Calling IsLarge.");
    return Store.IsLarge(quantity);
  }
}