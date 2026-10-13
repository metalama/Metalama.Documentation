namespace Doc.GenericTemplateShared;
[TraceSerialization]
public class Exporter
{
  public string ExportNumber(int value) => MetalamaInterceptors.Serializer_Serialize_Interceptor(value: value);
  public string ExportName(string value) => MetalamaInterceptors.Serializer_Serialize_Interceptor(value: value);
  public string ExportDate(System.DateTime value) => MetalamaInterceptors.Serializer_Serialize_Interceptor(value: value);
}
public static class Serializer
{
  public static string Serialize<T>(T value) => value?.ToString() ?? "null";
}