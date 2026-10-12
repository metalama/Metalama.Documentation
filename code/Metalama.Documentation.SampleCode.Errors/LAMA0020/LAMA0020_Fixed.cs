// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0020.Fixed;

internal static class Logging
{
    public static class ConsoleLogger
    {
        public static void Write( string message ) => Console.WriteLine( message );
    }
}

internal class Calculator
{
    [Log]
    public int Add( int a, int b ) => a + b;
}
