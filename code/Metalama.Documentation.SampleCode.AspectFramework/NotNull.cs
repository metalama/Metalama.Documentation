// This is public domain Metalama sample code.

using System;

namespace Doc.NotNull;

internal class Foo
{
    public void Method1( [NotNull] string s ) { }

    public void Method2( [NotNull] out string s )
    {
        s = null!;
    }

    [return: NotNull]
    public string Method3()
    {
        return null!;
    }

    [NotNull]
    public string Property { get; set; }
}

public class PostConditionFailedException : Exception
{
    public PostConditionFailedException( string message ) : base( message ) { }
}