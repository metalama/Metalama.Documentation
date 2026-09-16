// This is public domain Metalama sample code.

using System;

namespace Doc.OverrideConstructor;

[LogConstructors]
internal class SomeClass
{
    private readonly string _name;

    public SomeClass() : this( "" )
    {
        Console.WriteLine( "Within constructor A." );
    }

    public SomeClass( string name )
    {
        this._name = name;
        Console.WriteLine( "Within constructor B." );
    }
}

internal static class Program
{
    public static void Main()
    {
        _ = new SomeClass();
    }
}