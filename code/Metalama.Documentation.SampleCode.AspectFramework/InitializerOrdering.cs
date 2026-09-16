// This is public domain Metalama sample code.

using System;

namespace Doc.InitializerOrdering;

[AspectA]
[AspectB]
public partial class BaseClass
{
    public BaseClass()
    {
        Console.WriteLine( "BaseClass constructor" );
    }
}

public partial class DerivedClass : BaseClass
{
    public DerivedClass()
    {
        Console.WriteLine( "DerivedClass constructor" );
    }
}
