using System;
using Doc.GenericSpecialized;
internal static class MetalamaInterceptors
{
  internal static string Serializer_Serialize_Interceptor(int value)
  {
    Console.WriteLine("Serializing a value of type int.");
    return Serializer.Serialize(value);
  }
  internal static string Serializer_Serialize_Interceptor1(string value)
  {
    Console.WriteLine("Serializing a value of type string.");
    return Serializer.Serialize(value);
  }
}