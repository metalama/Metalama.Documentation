// This is public domain Metalama sample code.

namespace Doc.LogParameters;

internal class Foo
{
    [Log]
    private void VoidMethod( int a, out int b )
    {
        b = a;
    }

    [Log]
    private int IntMethod( int a )
    {
        return a;
    }
}