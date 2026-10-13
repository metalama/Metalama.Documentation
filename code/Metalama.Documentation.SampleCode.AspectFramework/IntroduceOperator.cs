// This is public domain Metalama sample code.

using System;

namespace Doc.IntroduceOperator;

[Equatable]
internal partial class Person
{
    public string Name { get; }

    public int Age { get; }

    public Person( string name, int age )
    {
        Name = name;
        Age = age;
    }
}

internal class Program
{
    private static void Main()
    {
        var p1 = new Person( "Alice", 30 );
        var p2 = new Person( "Alice", 30 );
        var p3 = new Person( "Bob", 25 );

#if METALAMA
        Console.WriteLine( $"p1 == p2: {p1 == p2}" );
        Console.WriteLine( $"p1 != p3: {p1 != p3}" );
        Console.WriteLine( $"p1.GetHashCode() == p2.GetHashCode(): {p1.GetHashCode() == p2.GetHashCode()}" );
#endif
    }
}
