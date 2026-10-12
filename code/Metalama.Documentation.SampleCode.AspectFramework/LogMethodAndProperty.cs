// This is public domain Metalama sample code.

namespace Doc.LogMethodAndProperty;

internal class Foo
{
    [Log]
    public int Method( int a, int b )
    {
        return a + b;
    }

    [Log]
    public int Property { get; set; }

    [Log]
    public string? Field;
}