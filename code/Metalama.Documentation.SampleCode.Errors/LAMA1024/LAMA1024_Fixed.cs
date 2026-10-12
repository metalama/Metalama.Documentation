// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA1024.Fixed;

public static class Printer
{
    public static void Print( string value ) => Console.WriteLine( value );
}

public static class Helpers { }

[InterceptPrint]
public static class Report
{
    public static void Run() => Printer.Print( "Hello" );
}
