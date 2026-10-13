// This is public domain Metalama sample code.

using System;

namespace Doc.GenericAddedTypeParameter;

[TraceLogging]
public class OrderService
{
    public void Place( int quantity, string product )
    {
        Log.Write( quantity );
        Log.Write( product );
    }
}

public static class Log
{
    // The parameter is an object, so a value type is boxed at the call site.
    public static void Write( object value ) => Console.WriteLine( value );
}

internal class Program
{
    private static void Main() => new OrderService().Place( 3, "Book" );
}
