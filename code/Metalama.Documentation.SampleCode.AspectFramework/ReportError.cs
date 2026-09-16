// This is public domain Metalama sample code.

namespace Doc.ReportError;

internal class Program
{
    // Intentionally omitting the _logger field so an error is reported.

    [Log]
    private void Foo() { }

    private static void Main()
    {
        new Program().Foo();
    }
}