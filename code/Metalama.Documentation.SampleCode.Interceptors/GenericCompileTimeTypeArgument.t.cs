namespace Doc.GenericCompileTimeTypeArgument;
[ValidateSerialization]
public class Exporter
{
  public string ExportNumber(int value) => MetalamaInterceptors.Serializer_Serialize_Interceptor(value: value);
  public string ExportName(string? value) => MetalamaInterceptors.Serializer_Serialize_Interceptor1(value: value);
}
public static class Serializer
{
  public static string Serialize<T>(T value) => value?.ToString() ?? "null";
}