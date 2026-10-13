// This is public domain Metalama sample code.

namespace Doc.GenericTemplateShared;

[TraceSerialization]
public class Exporter
{
    public string ExportNumber( int value ) => Serializer.Serialize( value );

    public string ExportName( string value ) => Serializer.Serialize( value );

    public string ExportDate( System.DateTime value ) => Serializer.Serialize( value );
}

public static class Serializer
{
    public static string Serialize<T>( T value ) => value?.ToString() ?? "null";
}
