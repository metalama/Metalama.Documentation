// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA1016.Fixed;

public static class Printer
{
    public static void Print( string value ) => Console.WriteLine( value );
}

[InterceptPrint]
public static class Report
{
    public static void Run() => Printer.Print( "Hello" );
}
