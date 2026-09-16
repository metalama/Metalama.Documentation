// This is public domain Metalama sample code.

using System;

namespace Doc.ProjectFabric_TwoAspects;

internal class Class1
{
    public void Method1()
    {
        Console.WriteLine( "Inside Class1.Method1" );
    }

    public void Method2()
    {
        Console.WriteLine( "Inside Class1.Method2" );
    }
}

public class Class2
{
    public void Method1()
    {
        Console.WriteLine( "Inside Class2.Method1" );
    }

    public void Method2()
    {
        Console.WriteLine( "Inside Class2.Method2" );
    }
}