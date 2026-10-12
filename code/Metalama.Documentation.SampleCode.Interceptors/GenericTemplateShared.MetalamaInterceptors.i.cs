using System;
using Doc.GenericTemplateShared;
internal static class MetalamaInterceptors
{
  internal static string Serializer_Serialize_Interceptor<T>(T value)
  {
    Console.WriteLine($"Serializing a {typeof(T).Name}.");
    return Serializer.Serialize(value);
  }
}