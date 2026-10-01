// This is public domain Metalama sample code.

using System;
using System.Threading;

namespace Doc.IntroduceMethod;

[ToString]
internal class MyClass { }

internal static class IdGenerator
{
    private static int _nextId;

    public static int GetId() => Interlocked.Increment( ref _nextId );
}

internal class Program
{
    private static void Main()
    {
        Console.WriteLine( new MyClass().ToString() );
        Console.WriteLine( new MyClass().ToString() );
        Console.WriteLine( new MyClass().ToString() );
    }
}