using System;
using Doc.GenericCompileTimeTypeArgument;
internal static class MetalamaInterceptors
{
  internal static string Serializer_Serialize_Interceptor(int value)
  {
    Console.WriteLine($"Serializing a value of type {typeof(int).Name}.");
    return Serializer.Serialize(value);
  }
  internal static string Serializer_Serialize_Interceptor1(string? value)
  {
    if (value == null)
    {
      throw new ArgumentNullException("value");
    }
    return Serializer.Serialize<string?>(value);
  }
}