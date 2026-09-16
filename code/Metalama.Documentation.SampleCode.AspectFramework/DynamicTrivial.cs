// This is public domain Metalama sample code.

using System;
using System.IO;

namespace Doc.DynamicTrivial;

internal class Program
{
    private TextWriter _logger = Console.Out;

    [Log]
    private void Foo() { }

    private static void Main()
    {
        new Program().Foo();
    }
}