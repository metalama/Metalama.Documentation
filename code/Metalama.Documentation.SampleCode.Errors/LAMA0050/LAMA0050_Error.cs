// This is public domain Metalama sample code.

namespace Doc.LAMA0050.Error;

// Error: the weaver required by the [Virtualize] aspect is not defined in the project or in its references.
[Virtualize]
internal class Calculator
{
    public int Add( int a, int b ) => a + b;
}
