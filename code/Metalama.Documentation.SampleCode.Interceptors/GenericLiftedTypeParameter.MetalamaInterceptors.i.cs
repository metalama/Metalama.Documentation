using System;
using System.Collections.Generic;
internal static class MetalamaInterceptors
{
  internal static void List_Add_Interceptor<T>(List<T> receiver, T item)
  {
    Console.WriteLine($"Adding a {typeof(T).Name}.");
    receiver.Add(item);
  }
}