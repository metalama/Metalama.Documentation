// This is public domain Metalama sample code.

namespace Doc.PrintFieldValues;

internal class Foo
{
    private readonly int _a;

    public string? B { get; set; }

    private static readonly int _c;

    [PrintFieldValues]
    public void Bar() { }
}