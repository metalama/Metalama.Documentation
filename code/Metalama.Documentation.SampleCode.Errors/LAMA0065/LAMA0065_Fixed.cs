// This is public domain Metalama sample code.

namespace Doc.LAMA0065.Fixed;

internal class Calculator
{
    [Log]
    public int Add( int a, int b ) => MathHelper.Sum( a, b );
}

// This static class doesn't satisfy the eligibility conditions of LoggingOptions.
internal static class MathHelper
{
    public static int Sum( int a, int b ) => a + b;
}
