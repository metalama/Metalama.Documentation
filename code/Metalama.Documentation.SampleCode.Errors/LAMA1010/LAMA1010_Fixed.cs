// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA1010.Fixed;

internal static class Printer
{
    public static void Print( string value ) => Console.WriteLine( value );
}

internal static class Program
{
    public static void Run() => Printer.Print( "Hello" );
}
