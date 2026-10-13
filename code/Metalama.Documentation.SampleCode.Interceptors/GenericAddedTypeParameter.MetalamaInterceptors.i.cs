using System;
using Doc.GenericAddedTypeParameter;
internal static class MetalamaInterceptors
{
  internal static void Log_Write_Interceptor<TValue>(TValue value)
  {
    Console.WriteLine($"Logging a {typeof(TValue).Name}.");
    Log.Write((object)value);
  }
}