// This is public domain Metalama sample code.

namespace Doc.EligibilityAndValidation;

internal class SomeClass
{
    private object? _logger;

    [Log]
    private void InstanceMethod() { }

    [Log]
    private static void StaticMethod() { }
}