// This is public domain Metalama sample code.

using System;
using System.IO;

namespace Doc.Eligibility;

internal class SomeClass
{
    private TextWriter _logger = Console.Out;

    [Log]
    private void InstanceMethod() { }

    [Log]
    private static void StaticMethod() { }
}